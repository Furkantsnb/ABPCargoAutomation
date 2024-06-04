using AutoMapper;
using CargoAutomation.Agentas;
using CargoAutomation.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace CargoAutomation.Lines
{
    public class LineManager : DomainService
    {
        private readonly IRepository<Agenta, Guid> _agentaRepository;
        private readonly IRepository<Line, Guid> _lineRepository;
    
        private readonly IMapper _mapper;
        private readonly IRepository<CargoAutomation.TransferCenters.TransferCenter, Guid> _transferCenterRepository;
        private readonly IStringLocalizer<CargoAutomationResource> _localizer;
    }
}
