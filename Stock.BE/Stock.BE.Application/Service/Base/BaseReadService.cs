using AutoMapper;
using ESP.Cloud.BE.Application.Interface.Base;
using ESP.Cloud.BE.Core.BaseDL.Repository;

namespace ESP.Cloud.BE.Application.Service.Base
{
    public class BaseReadService<TEntity> : IBaseReadService<TEntity>
    {
        private readonly IBaseReadRepository<TEntity> _baseReadRepository;
        protected readonly IMapper _mapper;
        public BaseReadService(IBaseReadRepository<TEntity> baseReadRepository, IMapper mapper)
        {
            _baseReadRepository = baseReadRepository;
            _mapper = mapper;
        }
        public async Task<List<TEntity>> GetAllAsync()
        {
            var result = await _baseReadRepository.GetAllAsync();

            return result;
        }

        public async Task<TEntity?> GetAsync(Guid id)
        {
            var result = await _baseReadRepository.GetByIdAsync(id);

            return result;
        }
    }
}
