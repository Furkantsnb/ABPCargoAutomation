using AutoMapper;
using Entities.Dtos.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CargoAutomation.Stations
{
   
        public class StationAppService : ApplicationService, IStationAppService
        {
            private readonly StationManager _stationManager;
            private readonly IMapper _mapper;

            public StationAppService(StationManager stationManager, IMapper mapper)
            {
                _stationManager = stationManager;
                _mapper = mapper;
            }

            public async Task<StationDto> CreateAsync(CreateStationDto createStationDto)
            {
                return await _stationManager.CreateAsync(createStationDto);
            }

            public async Task CreateRangeAsync(List<CreateStationDto> createStationDtos)
            {
                await _stationManager.CreateRangeAsync(createStationDtos);
            }

            public async Task<StationDto> UpdateAsync(Guid stationId, UpdateStationDto updateStationDto)
            {
                return await _stationManager.UpdateAsync(stationId, updateStationDto);
            }

            public async Task DeleteAsync(Guid stationId)
            {
                await _stationManager.DeleteAsync(stationId);
            }

        

            public async Task<StationDto> GetByIdAsync(Guid stationId)
            {
                return await _stationManager.GetByIdAsync(stationId);
            }

            public async Task<PagedResultDto<StationDto>> GetListAsync(PagedAndSortedResultRequestDto input)
            {
                return await _stationManager.GetListAsync(input);
            }
        }
}
