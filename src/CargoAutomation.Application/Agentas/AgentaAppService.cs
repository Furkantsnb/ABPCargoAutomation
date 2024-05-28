using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using AutoMapper;
using MyProject.Business.Abstract;
using Volo.Abp.Domain.Repositories;
using Entities.Dtos.Agentas;
using MyProject.Entities;
using System.Collections.Generic;
using Volo.Abp;
using Business.Constans;

namespace MyProject.Business.Concrete
{
    public class AgentaAppService : ApplicationService, IAgentaAppService
    {
        private readonly IRepository<Agenta, Guid> _agentaRepository;
        private readonly IRepository<TransferCenter, Guid> _transferCenterRepository;
        private readonly IMapper _mapper;
      

        public AgentaAppService(
            IRepository<Agenta, Guid> agentaRepository,
            IRepository<TransferCenter, Guid> transferCenterRepository,
            IMapper mapper)
        {
            _agentaRepository = agentaRepository;
            _transferCenterRepository = transferCenterRepository;
            _mapper = mapper;
        }

        public async Task<AgentaDto> CreateAsync(CreateAgentaDto input)
        {
            var agenta = _mapper.Map<Agenta>(input);
            var existingAgenta = await _agentaRepository.FirstOrDefaultAsync(a => a.UnitName == agenta.UnitName);

            if (existingAgenta != null)
            {
                throw new BusinessException(Messages.AgentaAlreadyExists);
            }

            var transferCenter = await _transferCenterRepository.FirstOrDefaultAsync(a => a.Id == agenta.TransferCenterId);
            if (transferCenter == null)
            {
                throw new BusinessException("Invalid Transfer Center ID.");
            }

            await _agentaRepository.InsertAsync(agenta);
            await CurrentUnitOfWork.SaveChangesAsync();

            return _mapper.Map<AgentaDto>(agenta);
        }

        public async Task<AgentaDto> UpdateAsync(Guid id, UpdateAgentaDto input)
        {
            var agenta = await _agentaRepository.GetAsync(id);
            if (agenta == null)
            {
                throw new BusinessException();
            }

            _mapper.Map(input, agenta);
            await _agentaRepository.UpdateAsync(agenta);
            await CurrentUnitOfWork.SaveChangesAsync();

            return _mapper.Map<AgentaDto>(agenta);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _agentaRepository.DeleteAsync(id);
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            var agenta = await _agentaRepository.GetAsync(id);
            if (agenta == null)
            {
                throw new BusinessException(Messages.AgentaNotFound);
            }
            agenta.IsDeleted = true;
            await _agentaRepository.UpdateAsync(agenta);
        }

        public async Task<AgentaDto> GetAsync(Guid id)
        {
            var agenta = await _agentaRepository.GetAsync(id);
            return _mapper.Map<AgentaDto>(agenta);
        }

        public async Task<PagedResultDto<AgentaDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var query = await _agentaRepository.GetPagedListAsync(skipCount: input.SkipCount,maxResultCount: input.MaxResultCount,
                sorting: input.Sorting);

            var totalCount = await _agentaRepository.GetCountAsync();

            return new PagedResultDto<AgentaDto>(
                totalCount,
                ObjectMapper.Map<List<Agenta>, List<AgentaDto>>(query));
        }

        public async Task<bool> AgentaExistsAsync(Guid id)
        {
            return await _agentaRepository.AnyAsync(a => a.Id == id);
        }
    }
}
