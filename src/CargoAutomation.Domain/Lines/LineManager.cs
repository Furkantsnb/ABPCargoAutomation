using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using CargoAutomation.Agentas;
using CargoAutomation.Lines;
using CargoAutomation.Stations;
using CargoAutomation.Units;
using Entities.Dtos.Lines;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace CargoAutomation.Application
{
    public class LineManager : DomainService,ILineAppService
    {
        private readonly IRepository<Line, Guid> _lineRepository;
        private readonly IRepository<Station, Guid> _stationRepository;
        private readonly IRepository<CargoAutomation.TransferCenters.TransferCenter, Guid> _transferCenterRepository;
        private readonly IRepository<Agenta, Guid> _agentaRepository;
        private readonly IRepository<Unit, Guid> _unitRepository;
        private readonly IMapper _mapper;

        public LineManager(
            IRepository<Line, Guid> lineRepository,
            IRepository<Station, Guid> stationRepository,
            IRepository<CargoAutomation.TransferCenters.TransferCenter, Guid> transferCenterRepository,
            IRepository<Agenta, Guid> agentaRepository,
            IRepository<Unit, Guid> unitRepository,
            IMapper mapper)
        {
            _lineRepository = lineRepository;
            _stationRepository = stationRepository;
            _transferCenterRepository = transferCenterRepository;
            _agentaRepository = agentaRepository;
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public async Task<LineDto> CreateAsync(CreateLineDto input)
        {
            await ValidateCreateLineDtoAsync(input);

            var line = _mapper.Map<Line>(input);
            await _lineRepository.InsertAsync(line);

            var stations = GenerateStations(input, line);
            foreach (var station in stations)
            {
                await _stationRepository.InsertAsync(station);
            }

            return _mapper.Map<Line, LineDto>(line);
        }

        public async Task DeleteAsync(Guid id)
        {
            var line = await _lineRepository.GetAsync(id);
            if (line == null)
            {
                throw new UserFriendlyException("Line not found.");
            }

            var stations = await _stationRepository.GetListAsync(s => s.LineId == id);
            foreach (var station in stations)
            {
                await _stationRepository.DeleteAsync(station);
            }

            await _lineRepository.DeleteAsync(line);
        }

        public async Task<LineDto> GetAsync(Guid id)
        {
            var line = await _lineRepository.GetAsync(id);
            if (line == null)
            {
                throw new UserFriendlyException("Line not found.");
            }

            return _mapper.Map<Line, LineDto>(line);
        }

        public async Task<PagedResultDto<LineDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _lineRepository.GetQueryableAsync();
            var totalCount = await AsyncExecuter.CountAsync(queryable);

            var lines = await AsyncExecuter.ToListAsync(
                queryable.OrderBy(a=>a.LineName)
                         .Skip(input.SkipCount)
                         .Take(input.MaxResultCount)
            );

            var lineDtos = _mapper.Map<List<Line>, List<LineDto>>(lines);

            return new PagedResultDto<LineDto>(
                totalCount,
                lineDtos
            );
        }

        public async Task<LineDto> UpdateAsync(Guid id, UpdateLineDto input)
        {
             await ValidateUpdateLineDtoAsync(input);
           

            var line = await _lineRepository.GetAsync(id);
            if (line == null)
            {
                throw new UserFriendlyException("Line not found.");
            }

            _mapper.Map(input, line);
            await _lineRepository.UpdateAsync(line);

            var existingStations = await _stationRepository.GetListAsync(s => s.LineId == id);
            foreach (var station in existingStations)
            {
                await _stationRepository.DeleteAsync(station);
            }

            var newStations = GenerateStations(input, line);
            foreach (var station in newStations)
            {
                await _stationRepository.InsertAsync(station);
            }

            return _mapper.Map<Line, LineDto>(line);
        }
        public async Task SoftDeleteAsync(Guid id)
        {
            var line = await _lineRepository.GetAsync(id);
            if (line == null)
            {
                throw new UserFriendlyException("Line not found.");
            }

            line.IsActive = true;
            await _lineRepository.UpdateAsync(line, true);
        }

        private async Task ValidateCreateLineDtoAsync(CreateLineDto input)
        {
            var existingLine = await _lineRepository.FirstOrDefaultAsync(l => l.LineName == input.LineName && l.LineType == input.LineType);
            if (existingLine != null)
            {
                throw new UserFriendlyException("This line already exists.");
            }

            if (input.TransferCenterId.HasValue)
            {
                var transferCenter = await _transferCenterRepository.GetAsync(input.TransferCenterId.Value);
                if (transferCenter == null)
                {
                    throw new UserFriendlyException("Invalid transfer center ID.");
                }
            }

            foreach (var stationId in input.Stations)
            {
                var unit = await _unitRepository.GetAsync(stationId);
                if (unit == null || (input.TransferCenterId.HasValue && input.TransferCenterId.Value == stationId))
                {
                    throw new UserFriendlyException($"Invalid station ID: {stationId}");
                }
            }

            if (input.LineType == LineType.AraHat)
            {
                if (!input.TransferCenterId.HasValue || input.Stations.FirstOrDefault() != input.TransferCenterId)
                {
                    throw new UserFriendlyException("First station for AraHat must be a transfer center.");
                }

                foreach (var stationId in input.Stations.Skip(1))
                {
                    var agenta = await _agentaRepository.GetAsync(tc => tc.Id == stationId);
                    if (agenta == null)
                    {
                        throw new UserFriendlyException("Other stations for AraHat must be agentas.");
                    }
                }
            }
            else if (input.LineType == LineType.AnaHat)
            {
                foreach (var stationId in input.Stations)
                {
                    var transferCenter = await _transferCenterRepository.GetAsync(tc => tc.Id == stationId);
                    if (transferCenter == null)
                    {
                        throw new UserFriendlyException("All stations for AnaHat must be transfer centers.");
                    }
                }
            }
        }

        private async Task ValidateUpdateLineDtoAsync(UpdateLineDto input)
        {
            var existingLine = await _lineRepository.FirstOrDefaultAsync(l => l.LineName == input.LineName && l.LineType == input.LineType);
            if (existingLine != null && existingLine.Id != input.Guid)
            {
                throw new UserFriendlyException("This line already exists.");
            }

            if (input.TransferCenterId.HasValue)
            {
                var transferCenter = await _transferCenterRepository.GetAsync(input.TransferCenterId.Value);
                if (transferCenter == null)
                {
                    throw new UserFriendlyException("Invalid transfer center ID.");
                }
            }

            foreach (var stationId in input.Stations)
            {
                var unit = await _unitRepository.GetAsync(stationId);
                if (unit == null || (input.TransferCenterId.HasValue && input.TransferCenterId.Value == stationId))
                {
                    throw new UserFriendlyException($"Invalid station ID: {stationId}");
                }
            }

            if (input.LineType == LineType.AraHat)
            {
                if (!input.TransferCenterId.HasValue || input.Stations.FirstOrDefault() != input.TransferCenterId)
                {
                    throw new UserFriendlyException("First station for AraHat must be a transfer center.");
                }

                foreach (var stationId in input.Stations.Skip(1))
                {
                    var agenta = await _agentaRepository.GetAsync(a => a.Id == stationId);
                    if (agenta == null)
                    {
                        throw new UserFriendlyException("Other stations for AraHat must be agentas.");
                    }
                }
            }
            else if (input.LineType == LineType.AnaHat)
            {
                foreach (var stationId in input.Stations)
                {
                    var transferCenter = await _transferCenterRepository.GetAsync(tc => tc.Id == stationId);
                    if (transferCenter == null)
                    {
                        throw new UserFriendlyException("All stations for AnaHat must be transfer centers.");
                    }
                }
            }
        }

        private List<Station> GenerateStations(CreateLineDto createLineDto, Line line)
        {
            var stations = new List<Station>();

            if (createLineDto.TransferCenterId.HasValue)
            {
                var transferStation = CreateTransferStation(createLineDto, line);
                stations.Add(transferStation);
            }

            for (int i = 0; i < createLineDto.Stations.Count; i++)
            {
                var stationUnitId = createLineDto.Stations[i];
                var station = CreateStation(line, stationUnitId, i + 1);
                stations.Add(station);
            }

            return stations;
        }

        private List<Station> GenerateStations(UpdateLineDto updateLineDto, Line line)
        {
            var stations = new List<Station>();

            if (updateLineDto.TransferCenterId.HasValue)
            {
                var transferStation = CreateTransferStation(updateLineDto, line);
                stations.Add(transferStation);
            }

            for (int i = 0; i < updateLineDto.Stations.Count; i++)
            {
                var stationUnitId = updateLineDto.Stations[i];
                var station = CreateStation(line, stationUnitId, i + 1);
                stations.Add(station);
            }

            return stations;
        }

        private Station CreateTransferStation(CreateLineDto createLineDto, Line line)
        {
            return new Station
            {
                StationName = $"{line.LineName}",
                OrderNumber = 1,
                LineId = line.Id,
                IsActive = true,
                UnitId = createLineDto.TransferCenterId.Value
            };
        }

        private Station CreateTransferStation(UpdateLineDto updateLineDto, Line line)
        {
            return new Station
            {
                StationName = $"{line.LineName}",
                OrderNumber = 1,
                LineId = line.Id,
                IsActive = true,
                UnitId = updateLineDto.TransferCenterId.Value
            };
        }

        private Station CreateStation(Line line, Guid stationUnitId, int orderNumber)
        {
            return new Station
            {
                StationName = $"{line.LineName} Station{orderNumber}",
                OrderNumber = orderNumber + 1,
                LineId = line.Id,
                IsActive = true,
                UnitId = stationUnitId
            };
        }
    }
}
