using AutoMapper;
using AutoMapper.Internal.Mappers;
using CargoAutomation.Localization;
using CargoAutomation.Stations;
using Entities.Dtos.Agentas;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Linq;

using Volo.Abp.Localization;

namespace CargoAutomation.Agentas
{
    public class AgentaManager : DomainService
    {
        private readonly IRepository<Agenta, Guid> _agentaRepository;
        private readonly IRepository<Station, Guid> _stationRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<CargoAutomation.TransferCenters.TransferCenter, Guid> _transferCenterRepository;
        private readonly IStringLocalizer<CargoAutomationResource> _localizer;

        public AgentaManager(
            IRepository<Agenta, Guid> agentaRepository,
            IMapper mapper, IRepository<Station, Guid> stationRepository,
            IRepository<TransferCenters.TransferCenter, Guid> transferCenterRepository,
            IStringLocalizer<CargoAutomationResource> localizer)
        {
            _agentaRepository = agentaRepository;
            _mapper = mapper;
            _transferCenterRepository = transferCenterRepository;
            _localizer = localizer;
            _stationRepository = stationRepository;
        }

        public async Task<AgentaDto> CreateAsync(CreateAgentaDto input)
        {
            var agenta = _mapper.Map<Agenta>(input);
             await _agentaRepository.FirstOrDefaultAsync(a => a.UnitName == agenta.UnitName);

            var transferCenter = await _transferCenterRepository.FirstOrDefaultAsync(a => a.Id == agenta.TransferCenterId);
            if (transferCenter == null)
            {
                throw new BusinessException("Invalid Transfer Center ID.");
            }

            await _agentaRepository.InsertAsync(agenta, true);

            return _mapper.Map<AgentaDto>(agenta);
        }

        public async Task<AgentaDto> UpdateAsync(Guid id, UpdateAgentaDto input)
        {
            var agenta = await _agentaRepository.GetAsync(id);
            _mapper.Map(input, agenta);
            await _agentaRepository.UpdateAsync(agenta, true);

            return _mapper.Map<AgentaDto>(agenta);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _agentaRepository.GetAsync(id);
            // Eğer TransferCenter bağlı bir istasyon varsa istasyonu sil
            var relatedStation = await _stationRepository.GetAsync(s => s.UnitId == id);
            if (relatedStation != null)
            {
                await _stationRepository.DeleteAsync(relatedStation);
            }
            await _agentaRepository.DeleteAsync(id);
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            var agenta = await _agentaRepository.GetAsync(id);
            agenta.IsDeleted = true;
            await _agentaRepository.UpdateAsync(agenta);
        }

        public async Task<AgentaDto> GetAsync(Guid id)
        {
            var agenta = await _agentaRepository.GetAsync(id);
          
            return _mapper.Map<Agenta, AgentaDto>(agenta);
        }


        public async Task<PagedResultDto<AgentaDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var agentas = await _agentaRepository.GetListAsync();
            var query = agentas.AsQueryable()
                .OrderBy(a => a.Id)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = agentas.Count;

            return new PagedResultDto<AgentaDto>(
                totalCount,
                _mapper.Map<List<Agenta>, List<AgentaDto>>(query)
            );
        }


        public async Task<bool> AgentaExistsAsync(Guid id)
        {
            return await _agentaRepository.AnyAsync(a => a.Id == id);
        }
    }
}
