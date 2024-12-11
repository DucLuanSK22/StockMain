namespace ESP.Cloud.BE.Application.Interface.Base
{
    public interface IBaseReadService<TEntityDto>
    {
        /// <summary>
        /// Hàm lấy một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>bản ghi</returns>
        /// Created by: nttue(14/02/2024)
        Task<TEntityDto?> GetAsync(Guid id);

        /// <summary>
        /// Hàm lấy cả bản ghi của một bảng
        /// </summary>
        /// <returns>Trả về tất cả các bản ghi</returns>
        /// Created by: nttue(14/02/20234 
        Task<List<TEntityDto>> GetAllAsync();
    }
}
