using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using AutoMapper;
using Entities.Dtos.Agentas;
using System.Collections.Generic;
using Volo.Abp;
using CargoAutomation.Localization.Constans;
using CargoAutomation.TransferCenters;
using System.Linq;
using CargoAutomation.TransferCenter;
using Entities.Dtos.TransferCenter;

namespace CargoAutomation.TransferCenters
{
    public class TransferCenterAppService : ApplicationService, ITransferCenterAppService
    {
        private readonly TransferCenterManager _manager;
        private readonly IMapper _mapper;

        public TransferCenterAppService(TransferCenterManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<TransferCenterDto> CreateAsync(CreateTransferCenterDto input)
        {
            return await _manager.CreateAsync(input);
        }

        public async Task<TransferCenterDto> UpdateAsync(Guid id, UpdateTransferCenterDto input)
        {
            return await _manager.UpdateAsync(id, input);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _manager.DeleteAsync(id);
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            await _manager.SoftDeleteAsync(id);
        }

        public async Task<TransferCenterDto> GetAsync(Guid id)
        {
            return await _manager.GetAsync(id);
        }

        public async Task<PagedResultDto<TransferCenterDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return await _manager.GetListAsync(input);
        }

        public async Task<bool> TransferCenterExistsAsync(Guid id)
        {
            return await _manager.TransferCenterExistsAsync(id);
        }

        public async Task<List<AgentaDto>> GetAgentasByTransferCenterIdAsync(Guid transferCenterId)
        {
            return await _manager.GetAgentasByTransferCenterIdAsync(transferCenterId);
        }
    }
}
