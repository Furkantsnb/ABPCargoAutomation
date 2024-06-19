using Entities.Dtos.Lines;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CargoAutomation.Lines
{
    public interface ILineAppService : ICrudAppService<LineDto,Guid,PagedAndSortedResultRequestDto,CreateLineDto,UpdateLineDto>
    {
        Task HartDeleteAsync(Guid id);
    }
}
