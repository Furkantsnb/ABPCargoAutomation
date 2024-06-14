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
      //      await _transferCenterRepository.GetAsync(p => p.Id == guid);
            //if (result == null)
            //{
            //    throw new UserFriendlyException("TransferCenter bulunamadı");
            //}

            var existingLine = await _lineRepository.GetAsync(l => l.LineName == createLineDto.LineName && l.LineType == createLineDto.LineType);
            if (existingLine != null)
            {
               
                throw new UserFriendlyException("Bu hat zaten eklenmiş.");
            }

            //foreach (var stationId in createLineDto.Stations)
            //{
            //     await _unitRepository.GetAsync(u => u.Id == stationId);
            //    //if (existingUnit == null || guid == stationId)
            //    //{
            //    //    throw new UserFriendlyException($"Girilen istasyonlar arasında fazla ya da geçersiz bir Station bulundu  : {stationId}");
                   
            //    //}
            //}

            if (createLineDto.LineType == LineType.AraHat)
            {
                //if (guid == null && createLineDto.Stations.FirstOrDefault() != guid)
                //{
                //    throw new UserFriendlyException("Ara hat için ilk durak transfer merkezi olmalıdır.");
                //}

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
           // var stations = GenerateStations(createLineDto, line);


            //   await _stationAppService.CreateRangeAsync(stations);

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

            if (lineToUpdate == null)
            {
                throw new UserFriendlyException("Hat bulunamadı");
            }

            // TransferCenterId kontrolü
            var transferCenter = await _transferCenterRepository.GetAsync(u => u.Id == input.TransferCenterId);
            if (transferCenter == null)
            {
                throw new UserFriendlyException("Geçersiz transfer merkezi IDsi");
            }

            // Girilen istasyonlar veri tabanında mevcut mu kontrol et
            foreach (var stationId in input.Stations)
            {
                var existingUnit = await _unitRepository.GetAsync(u => u.Id == stationId);
                if (existingUnit == null || input.TransferCenterId == stationId)
                {
                    throw new UserFriendlyException($"Girilen istasyonlar arasında fazla ya da geçersiz bir Station bulundu  : {stationId}");
                }
            }

            // Aynı hatın veri tabanında daha önce eklenip eklenmediğini kontrol et
            var existingLine = await _lineRepository.GetAsync(l => l.LineName == input.LineName && l.LineType == input.LineType && l.Id != id);
            if (existingLine != null)
            {
                throw new UserFriendlyException("Bu hat zaten eklenmiş.");
            }

            // AraHat için kontrol
            if (input.LineType == LineType.AraHat)
            {
                // İlk durak transferCenter olmalı
                if (input.TransferCenterId == null && input.Stations.FirstOrDefault() != input.TransferCenterId)
                {
                    throw new UserFriendlyException("Ara hat için ilk durak transfer merkezi olmalıdır.");
                }

                // Diğer duraklar sadece agentalardan oluşmalı
                foreach (var stationId in input.Stations.Skip(1))
                {
                    var unit = await _agentaRepository.GetAsync(u => u.Id == stationId);
                    if (unit == null || unit is not Agenta)
                    {
                        throw new UserFriendlyException("Ara hat için diğer duraklar sadece acentalardan oluşmalıdır.");
                    }
                }
            }
            else if (input.LineType == LineType.AnaHat)
            {
                // Tüm duraklar sadece TransferCenter olmalı
                foreach (var stationId in input.Stations)
                {
                    var unit = await _transferCenterRepository.GetAsync(u => u.Id == stationId);
                    if (unit == null || unit is not CargoAutomation.TransferCenters.TransferCenter)
                    {
                        throw new UserFriendlyException("Ana hat için tüm duraklar bir transfer merkezi olmalıdır.");
                    }
                }
            }

            //Line line = _mapper.Map<Line>(createLineDto);

            //await _lineRepository.InsertAsync(line);

            //var stations = GenerateStations(createLineDto, line);

            //await _stationAppService.CreateRangeAsync(stations);

            //return _mapper.Map<Line, LineDto>(line);
            // Update line properties
            
            // Update other properties as needed
            Line line = _mapper.Map<Line>(input);

            // Update line in database
            await _lineRepository.UpdateAsync(line);

            // Delete existing stations for this line
            var existingStations = await _stationRepository.GetListAsync(s => s.LineId == id);
            foreach (var station in existingStations)
            {
                await _stationRepository.DeleteAsync(station);
            }

            // Generate and add new stations
            var newStations = GenerateStationss(input, line);
          
           // await _stationAppService.CreateRangeAsync(newStations);

            return _mapper.Map<Line, LineDto>(line);
        }
        public async Task SoftDeleteAsync(Guid id)
        {
            var line = await _lineRepository.GetAsync(id);
            if (line == null)
            {
                throw new UserFriendlyException("Line not found.");
            }

            line.IsDeleted = true;
            await _lineRepository.UpdateAsync(line, true);
        }



        private List<Station> GenerateStationss(UpdateLineDto updateLineDto, Line line)
        {
            var stations = new List<Station>();

            if (updateLineDto.TransferCenterId != Guid.Empty)
            {
                var transferStation = CreateTransferStations(updateLineDto, line);
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
        private List<Station> GenerateStations(List<Guid> unitIds)
        {
            var stations = new List<Station>();

            //if (createLineDto.TransferCenterId != Guid.Empty)
            //{
            //    var transferStation = CreateTransferStation(createLineDto, line);
            //    stations.Add(transferStation);
            //}

            foreach (var item in unitIds)
            {
                var station = new Station()
                {
                    UnitId = item
                };
                stations.Add(station);
                
            }
            //for (int i = 0; i < createLineDto.Stations.Count; i++)
            //{
            //    var stationUnitId = createLineDto.Stations[i];
            //    var station = CreateStation(line, stationUnitId, i + 1);
            //    stations.Add(station);
            //}

            return stations;
        }

        private Station CreateTransferStation(CreateLineDto createLineDto, Line line)
        {
            return new Station
            {
                StationName = $"{line.LineName}",
                OrderNumber = 1,
                LineId = line.Id,
                UnitId = createLineDto.TransferCenterId.Value
            };
        }
        private Station CreateTransferStations(UpdateLineDto updateLineDto, Line line)
        {
            return new Station
            {
                StationName = $"{line.LineName}",
                OrderNumber = 1,
                LineId = line.Id,
                UnitId = updateLineDto.TransferCenterId.Value
            };
        }

        private Station CreateStation(Line line, Guid stationUnitId, int orderNumber)
        {
            return new Station
            {
                StationName = $"{line.LineName} Durak{orderNumber}",
                OrderNumber = orderNumber + 1,
                LineId = line.Id,
      
                UnitId = stationUnitId
            };
        }
    }
}
