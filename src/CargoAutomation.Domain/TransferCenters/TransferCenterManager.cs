using AutoMapper;
using CargoAutomation.Agentas;
using CargoAutomation.Localization;
using CargoAutomation.TransferCenter;
using Entities.Dtos.Agentas;
using Entities.Dtos.TransferCenter;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace CargoAutomation.TransferCenters
{
    public class TransferCenterManager : DomainService
    {
        private readonly IRepository<TransferCenter, Guid> _transferCenterRepository;
        private readonly IRepository<Agenta,Guid> _agentaRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CargoAutomationResource> _localizer;

        public TransferCenterManager(
            IRepository<TransferCenter, Guid> transferCenterRepository, IRepository<Agenta, Guid> agentaRepository,
            IMapper mapper,
            IStringLocalizer<CargoAutomationResource> localizer)
        {
            _transferCenterRepository = transferCenterRepository;
            _mapper = mapper;
            _localizer = localizer;
            _agentaRepository= agentaRepository;
        }

        public async Task<TransferCenterDto> CreateAsync(CreateTransferCenterDto input)
        {
            var transferCenter = _mapper.Map<TransferCenter>(input);
            var existingTransferCenter = await _transferCenterRepository.FirstOrDefaultAsync(tc =>
                tc.UnitName == transferCenter.UnitName &&
                tc.ManagerName == transferCenter.ManagerName &&
                tc.ManagerSurname == transferCenter.ManagerSurname);

            if (existingTransferCenter != null)
            {
                throw new UserFriendlyException(_localizer["TransferCenterAlreadyExists"]);
            }

            await _transferCenterRepository.InsertAsync(transferCenter, true);

            return _mapper.Map<TransferCenterDto>(transferCenter);
        }

        public async Task<TransferCenterDto> UpdateAsync(Guid id,UpdateTransferCenterDto input)
        {
            var transferCenter = await _transferCenterRepository.GetAsync(id);
            if (transferCenter == null)
            {
                throw new UserFriendlyException(_localizer["TransferCenterNotFound"]);
            }

            _mapper.Map(input, transferCenter);
            await _transferCenterRepository.UpdateAsync(transferCenter, true);

            return _mapper.Map<TransferCenterDto>(transferCenter);
        }

        public async Task DeleteAsync(Guid id)
        {
            var transferCenter = await _transferCenterRepository.GetAsync(id);
            if (transferCenter == null)
            {
                throw new UserFriendlyException(_localizer["TransferCenterNotFound"]);
            }

            await _transferCenterRepository.DeleteAsync(transferCenter);
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            var transferCenter = await _transferCenterRepository.GetAsync(id);
            if (transferCenter == null)
            {
                throw new UserFriendlyException(_localizer["TransferCenterNotFound"]);
            }

            transferCenter.IsDeleted = true;
            await _transferCenterRepository.UpdateAsync(transferCenter);
        }

        public async Task<TransferCenterDto> GetAsync(Guid id)
        {
            var transferCenter = await _transferCenterRepository.GetAsync(id);
            if (transferCenter == null)
            {
                throw new UserFriendlyException(_localizer["TransferCenterNotFound"]);
            }

            return _mapper.Map<TransferCenterDto>(transferCenter);
        }

        public async Task<PagedResultDto<TransferCenterDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var transferCenters = await _transferCenterRepository.GetListAsync();
            var query = transferCenters.AsQueryable()
                .OrderBy(tc => tc.Id)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = transferCenters.Count;

            return new PagedResultDto<TransferCenterDto>(
                totalCount,
                _mapper.Map<List<TransferCenter>, List<TransferCenterDto>>(query)
            );
        }

        public async Task<bool> TransferCenterExistsAsync(Guid id)
        {
            return await _transferCenterRepository.AnyAsync(tc => tc.Id == id);
        }

        public async Task<List<AgentaDto>> GetAgentasByTransferCenterIdAsync(Guid transferCenterId)
        {
            // İlgili transfer merkezine bağlı tüm agentaları getir
            var agentas = await _agentaRepository.GetListAsync(a => a.TransferCenterId == transferCenterId);

            // Eğer agentalar bulunamazsa, hata fırlat
            if (agentas == null || !agentas.Any())
            {
                throw new UserFriendlyException(_localizer["AgentasNotFoundForTransferCenter"]);
            }

            // Agentalar bulunursa, DTO'ya dönüştürüp geri döndür
            return _mapper.Map<List<AgentaDto>>(agentas);
        }


    }
}
