using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;

using Entities.Dtos.Agentas;

namespace CargoAutomation.Agentas
{
    public interface IAgentaAppService : IApplicationService
    {
        Task<AgentaDto> CreateAsync(CreateAgentaDto input);
        Task<AgentaDto> UpdateAsync(Guid id, UpdateAgentaDto input);
        Task DeleteAsync(Guid id);
        Task SoftDeleteAsync(Guid id);
        Task<AgentaDto> GetAsync(Guid id);
        Task<PagedResultDto<AgentaDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task<bool> AgentaExistsAsync(Guid id);
    }
}
