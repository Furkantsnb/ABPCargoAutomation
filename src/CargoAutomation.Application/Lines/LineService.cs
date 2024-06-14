using System;
using System.Threading.Tasks;
using CargoAutomation.Lines;
using Entities.Dtos.Lines;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CargoAutomation.Application
{
    public class LineService : ApplicationService, ILineAppService
    {
        private readonly LineManager _lineManager;

        public LineService(LineManager lineManager)
        {
            _lineManager = lineManager;
        }

        public async Task<LineDto> CreateAsync(CreateLineDto input,Guid guid)
        {
            return await _lineManager.CreateAsync(input,guid);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _lineManager.DeleteAsync(id);
        }

        public async Task<LineDto> GetAsync(Guid id)
        {
            return await _lineManager.GetAsync(id);
        }

        public async Task<PagedResultDto<LineDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return await _lineManager.GetListAsync(input);
        }

        public async Task<LineDto> UpdateAsync(Guid id, UpdateLineDto input)
        {
            return await _lineManager.UpdateAsync(id, input);
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            await _lineManager.SoftDeleteAsync(id);
        }
    }
}
