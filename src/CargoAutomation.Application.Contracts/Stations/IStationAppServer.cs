using Entities.Dtos.Stations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CargoAutomation.Stations
{
    
        public interface IStationAppService : IApplicationService
        {
            Task<StationDto> CreateAsync(CreateStationDto createStationDto);
            Task CreateRangeAsync(List<CreateStationDto> createStationDtos); // AddRange metodu
            Task<StationDto> UpdateAsync(Guid stationId, UpdateStationDto updateStationDto);
            Task DeleteAsync(Guid stationId);
            Task SoftDeleteAsync(Guid stationId);
            Task<StationDto> GetByIdAsync(Guid stationId);
            Task<PagedResultDto<StationDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        }

    
}
