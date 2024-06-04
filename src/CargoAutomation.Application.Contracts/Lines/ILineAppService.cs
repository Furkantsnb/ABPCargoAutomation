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
        Task<ListResultDto<LineDto>> GetListAsync();
        Task<LineDto> GetAsync(int id);
        Task<LineDto> CreateAsync(CreateLineDto input);
        Task UpdateAsync(int id, UpdateLineDto input);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
    }
}
