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

namespace CargoAutomation.Agentas
{
    public class AgentaAppService : ApplicationService, IAgentaAppService
    {
        private readonly AgentaManager _manager;
        private readonly IMapper _mapper;

        public AgentaAppService(AgentaManager manager, IMapper mapper)
        {
            _manager = manager;
            
            _mapper = mapper;
        }

        public async Task<AgentaDto> CreateAsync(CreateAgentaDto input)
        {
            return await _manager.CreateAsync(input);
        }

        public async Task<AgentaDto> UpdateAsync(Guid id, UpdateAgentaDto input)
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

        public async Task<AgentaDto> GetAsync(Guid id)
        {
            return await _manager.GetAsync(id);
        }

        public async Task<PagedResultDto<AgentaDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
          return await _manager.GetListAsync(input);
        }

        public async Task<bool> AgentaExistsAsync(Guid id)
        {
            return await _manager.AgentaExistsAsync(id);
        }
    }
}
