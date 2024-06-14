using Entities.Dtos.Lines;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CargoAutomation.Lines
{
    public interface ILineAppService : IApplicationService
    {
        Task<PagedResultDto<LineDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task<LineDto> GetAsync(Guid id);
        Task<LineDto> CreateAsync(CreateLineDto input,Guid guid);
        Task<LineDto> UpdateAsync(Guid id, UpdateLineDto input);
        Task DeleteAsync(Guid id);
        Task SoftDeleteAsync(Guid id);
    }
}
