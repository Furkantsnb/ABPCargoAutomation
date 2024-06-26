﻿using AutoMapper;
using Entities.Dtos.Stations;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using static System.Collections.Specialized.BitVector32;

namespace CargoAutomation.Stations
{
    public class StationManager : DomainService,IStationAppService
    {
        private readonly IRepository<Station, Guid> _stationRepository;
        private readonly IMapper _mapper;

        public StationManager(IRepository<Station, Guid> stationRepository, IMapper mapper)
        {
            _stationRepository = stationRepository;
            _mapper = mapper;
        }

        public async Task<StationDto> CreateAsync(CreateStationDto createStationDto)
        {
            var station = _mapper.Map<Station>(createStationDto);
            await _stationRepository.InsertAsync(station, true);
            return _mapper.Map<StationDto>(station);
        }

        public async Task CreateRangeAsync(List<CreateStationDto> createStationDtos)
        {
            var stations = _mapper.Map<List<Station>>(createStationDtos);
            await _stationRepository.InsertManyAsync(stations, autoSave: true);
        }



        public async Task<StationDto> UpdateAsync(Guid stationId, UpdateStationDto updateStationDto)
        {
            var station = await _stationRepository.GetAsync(stationId);
 
            _mapper.Map(updateStationDto, station);
            await _stationRepository.UpdateAsync(station, true);

            return _mapper.Map<StationDto>(station);
        }

        public async Task DeleteAsync(Guid stationId)
        {
            var station = await _stationRepository.GetAsync(stationId);

            await _stationRepository.DeleteAsync(station, true);
        }

   

        public async Task<StationDto> GetByIdAsync(Guid stationId)
        {
            var station = await _stationRepository.GetAsync(stationId);

            return _mapper.Map<StationDto>(station);
        }

        public async Task<PagedResultDto<StationDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _stationRepository.GetQueryableAsync();
            var query = queryable
                .OrderBy(a=>a.Id)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var stations = await AsyncExecuter.ToListAsync(query);
            var totalCount = await _stationRepository.GetCountAsync();

            return new PagedResultDto<StationDto>(
                totalCount,
                _mapper.Map<List<StationDto>>(stations)
            );
        }
    }
}
