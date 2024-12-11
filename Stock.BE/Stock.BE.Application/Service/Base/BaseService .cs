using AutoMapper;
using ESP.Cloud.BE.Application.Interface.Base;
using ESP.Cloud.BE.Core.BaseDL.Repository;
using ESP.Cloud.BE.Core.ESPException;

namespace ESP.Cloud.BE.Application.Service.Base
{
    public abstract class BaseService<TEntity, TEntityCreateDto, TEntityUpdateDto> : BaseReadService<TEntity>, IBaseService<TEntity, TEntityCreateDto, TEntityUpdateDto>
    {
        #region
        protected readonly IBaseRepository<TEntity> _baseRepository;
        #endregion

        #region
        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        #region
        /// <summary>
        /// Hàm chèn 1 bản ghi của một bảng
        /// </summary>
        /// <param name="entityCreateDto">dữ liệu bản ghi</param>
        /// <returns>Trả về bản ghi được chèn</returns>
        /// Created by: nttue(7/7/2023)
        public virtual async Task<TEntity> InsertAsync(TEntityCreateDto entityCreateDto)
        {

            var entityCreate = await MapCreateDtoToEntity(entityCreateDto);


            await _baseRepository.InsertAsync(entityCreate);
            await AfterInsertEntity(entityCreateDto);
            return entityCreate;
        }

        protected virtual async Task AfterInsertEntity(TEntityCreateDto entityCreateDto)
        {

        }

        /// <summary>
        /// Hàm sửa một nhân viên
        /// </summary>
        /// <param name="id">id của nhân viên được sửa</param>
        /// <param name="entityUpdateDto">thông tin sửa của nhân viên</param>
        /// <returns>Trả về nhân viên mới đã được sửa</returns>
        /// Created by: nttue(7/7/2023)

        public virtual async Task<TEntity> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {

            var entityUpdate = await MapUpdateDtoToEntity(id, entityUpdateDto);

            var entityUpdated = await _baseRepository.UpdateAsync(id, entityUpdate);
            return entityUpdate;
        }

        /// <summary>
        /// Xóa nhân viên theo từng id
        /// </summary>
        /// <param name="id">id của 1 nhân viên</param>
        /// <returns>Trả về số lượng bản ghi đã xóa</returns>
        /// Created by: nttue(7/7/2023)
        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            await _baseRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Hàm trừu tượng dùng để map EntityCreateDto sang Entity
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns>Trả về một entity</returns>
        /// Created by: nttue(7/7/2023)
        public abstract Task<TEntity> MapCreateDtoToEntity(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Hàm trừu tượng dùng để map EntityUpdateDto sang Entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entityUpdateDto"></param>
        /// <returns>Trả về một entity</returns>
        /// Created by: nttue(7/7/2023)
        public abstract Task<TEntity> MapUpdateDtoToEntity(Guid id, TEntityUpdateDto entityUpdateDto);

        #endregion
    }
}
