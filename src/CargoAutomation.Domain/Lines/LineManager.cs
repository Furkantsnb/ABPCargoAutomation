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
    public class LineManager : DomainService
    {
        private readonly IRepository<Line, Guid> _lineRepository;

        private readonly IRepository<Station, Guid> _stationRepository;
        private readonly IRepository<CargoAutomation.TransferCenters.TransferCenter, Guid> _transferCenterRepository;
        private readonly IRepository<Agenta, Guid> _agentaRepository;
        private readonly IRepository<Unit, Guid> _unitRepository;
        private readonly IStationAppService _stationAppService;
        private readonly IMapper _mapper;

        public LineManager(
            IRepository<Line, Guid> lineRepository,
            IRepository<Station, Guid> stationRepository,
            IRepository<CargoAutomation.TransferCenters.TransferCenter, Guid> transferCenterRepository,
            IRepository<Agenta, Guid> agentaRepository,
            IRepository<Unit, Guid> unitRepository,
            IStationAppService stationAppService,
            IMapper mapper)
        {
            _lineRepository = lineRepository;
            _stationRepository = stationRepository;
            _transferCenterRepository = transferCenterRepository;
            _agentaRepository = agentaRepository;
            _unitRepository = unitRepository;
            _mapper = mapper;
            _stationAppService = stationAppService;
        }

        public async Task<LineDto> CreateAsync(CreateLineDto createLineDto )
        {
     

            var existingLine = await _lineRepository.GetAsync(l => l.LineName == createLineDto.LineName && l.LineType == createLineDto.LineType);
            if (existingLine != null)
            {
               
                throw new UserFriendlyException("Bu hat zaten eklenmiş.");
            }

            if (createLineDto.LineType == LineType.AraHat)
            {
                
                foreach (var stationId in createLineDto.Stations.Skip(1))
                {
                    var unit = await _unitRepository.GetAsync(u => u.Id == stationId);
                    if (unit is not Agenta)
                    {
                        throw new UserFriendlyException("Ara hat için diğer duraklar sadece acentalardan oluşmalıdır.");
                    }
                }
            }
            else if (createLineDto.LineType == LineType.AnaHat)
            {
                foreach (var stationId in createLineDto.Stations)
                {
                    var unit = await _unitRepository.GetAsync(u => u.Id == stationId);
                    if (unit is not CargoAutomation.TransferCenters.TransferCenter)
                    {
                        throw new UserFriendlyException("Ana hat için tüm duraklar bir transfer merkezi olmalıdır.");
                    }
                }
            }


            Line line = _mapper.Map<Line>(createLineDto);
            line.Stations =  GenerateStations(createLineDto.Stations);
            await _lineRepository.InsertAsync(line);

            return  _mapper.Map<Line, LineDto>(line);
        }


        public async Task DeleteAsync(Guid id)
        {
            // Hat bilgisini getir
            var lineToDelete = await _lineRepository.GetAsync(l => l.Id == id);

            // Hat bulunamazsa hata döndür
            if (lineToDelete == null)
            {
                throw new UserFriendlyException("Hat bulunamadı");
            }

            // Hat silinirken, o hatta bağlı tüm istasyonları getir
            var stationsToDelete = await _stationRepository.GetListAsync(s => s.LineId == id);

            // Her bir istasyon için silme işlemi yap
            foreach (var station in stationsToDelete)
            {
                await _stationRepository.DeleteAsync(station);
            }

            // Hatı sil
            await _lineRepository.DeleteAsync(lineToDelete);

          
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

        public async Task<LineDto> UpdateAsync(Guid id ,UpdateLineDto input)
        {
            var lineToUpdate = await _lineRepository.GetAsync(l => l.Id == id);


            var existingLine = await _lineRepository.GetAsync(l => l.LineName == input.LineName && l.LineType == input.LineType);
            if (existingLine != null)
            {

                throw new UserFriendlyException("Bu hat zaten eklenmiş.");
            }

            if (input.LineType == LineType.AraHat)
            {

                foreach (var stationId in input.Stations.Skip(1))
                {
                    var unit = await _unitRepository.GetAsync(u => u.Id == stationId);
                    if (unit is not Agenta)
                    {
                        throw new UserFriendlyException("Ara hat için diğer duraklar sadece acentalardan oluşmalıdır.");
                    }
                }
            }
            else if (input.LineType == LineType.AnaHat)
            {
                foreach (var stationId in input.Stations)
                {
                    var unit = await _unitRepository.GetAsync(u => u.Id == stationId);
                    if (unit is not CargoAutomation.TransferCenters.TransferCenter)
                    {
                        throw new UserFriendlyException("Ana hat için tüm duraklar bir transfer merkezi olmalıdır.");
                    }
                }
            }
            var existingStations = await _stationRepository.GetListAsync(s => s.LineId == id);
            foreach (var station in existingStations)
            {
                await _stationRepository.DeleteAsync(station);
            }

            // Update other properties as needed
            Line line = _mapper.Map<Line>(input);

            line.Stations = GenerateStations(input.Stations);
            // Update line in database
            await _lineRepository.UpdateAsync(line);

            return _mapper.Map<Line, LineDto>(line);
        }
     



        private List<Station> GenerateStations(List<Guid> unitIds)
        {
            var stations = new List<Station>();

            foreach (var item in unitIds)
            {
                var station = new Station()
                {
                    UnitId = item
                };
                stations.Add(station);
                
            }

            return stations;
        }

    }
}
