namespace Stock.BE.Core.BaseDL.Repository
{
    public interface IBaseRepository<TEntity> : IBaseReadRepository<TEntity>
    {
        #region Methods
        /// <summary>
        /// Hàm chèn 1 bản ghi của một bảng
        /// </summary>
        /// <param name="entity">dữ liệu bản ghi</param>
        /// <returns>Trả về bản ghi được chèn</returns>
        /// Created by: nttue(14/02/202)
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// Hàm chèn nhiều bản ghi của một bảng
        /// </summary>
        /// <param name="entity">dữ liệu bản ghi</param>
        /// <returns>Trả về bản ghi được chèn</returns>
        /// Created by: nttue(14/02/202)
        Task InsertBatchAsync(List<TEntity> entity);

        /// <summary>
        /// Hàm cập nhật 1 bản ghi của một bảng
        /// </summary>
        /// <param name="id"> id của bản ghi</param>
        /// <param name="entity">dữ liệu bản ghi</param>
        /// <returns>Trả về bản ghi được cập nhật</returns>
        /// Created by: nttue(13/07/2023) 
        Task<TEntity> UpdateAsync(Guid id, TEntity entity);

        /// <summary>
        /// Hàm xóa 1 bản ghi của một bảng
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <returns>Trả về số bản ghi đã xóa</returns>
        /// Created by: nttue(13/07/2023) 
        Task DeleteAsync(Guid id);
        #endregion
    }
}
