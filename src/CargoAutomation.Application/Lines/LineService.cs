using System;
using System.Threading.Tasks;
using CargoAutomation.Lines;
using Entities.Dtos.Lines;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CargoAutomation.Application
{
    public class LineService : CrudAppService<Line,LineDto,Guid,PagedAndSortedResultRequestDto,CreateLineDto,UpdateLineDto>, ILineAppService
    {
        private readonly LineManager _lineManager;

        public LineService(IRepository<Line, Guid> repository, LineManager lineManager) : base(repository)
        {
            _lineManager = lineManager;
        }

        public override async Task<LineDto> CreateAsync(CreateLineDto input)
        {
            return await _lineManager.CreateAsync(input);
        }
        public override  async Task<LineDto> UpdateAsync(Guid id, UpdateLineDto input)
        {
            return await _lineManager.UpdateAsync(id,input);
        }
        public override Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }
        public override Task<LineDto> GetAsync(Guid id)
        {
            return base.GetAsync(id);
        }
        public override Task<PagedResultDto<LineDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return base.GetListAsync(input);
        }

        public async Task HartDeleteAsync(Guid id)
        {
            await Repository.HardDeleteAsync(c=>c.Id==id);
        }
    }
}
