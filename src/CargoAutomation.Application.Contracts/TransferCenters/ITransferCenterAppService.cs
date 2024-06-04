using CargoAutomation.TransferCenter;
using Entities.Dtos.Agentas;
using Entities.Dtos.TransferCenter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CargoAutomation.TransferCenters
{
   
        public interface ITransferCenterAppService : IApplicationService
        {
            Task<TransferCenterDto> CreateAsync(CreateTransferCenterDto input);
            Task<TransferCenterDto> UpdateAsync(Guid id,UpdateTransferCenterDto input);
            Task DeleteAsync(Guid id);
            Task SoftDeleteAsync(Guid id);
            Task<TransferCenterDto> GetAsync(Guid id);
            Task<PagedResultDto<TransferCenterDto>> GetListAsync(PagedAndSortedResultRequestDto input);
            Task<bool> TransferCenterExistsAsync(Guid id);
            Task<List<AgentaDto>> GetAgentasByTransferCenterIdAsync(Guid transferCenterId);
        }
    
}
