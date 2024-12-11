using System.ComponentModel.DataAnnotations;

namespace Stock.BE.Application.Dto
{
    public class UserDto
    {
        [Key]
        public Guid user_id { get; set; }
        /// <summary>
        /// Tên đăng nhập của người dùng
        /// </summary>
        public string fullname { get; set; } = string.Empty;
        /// <summary>
        /// Email người dùng
        /// </summary>
        public string email { get; set; } = string.Empty;

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string phone { get; set; } = string.Empty;

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [MaxLength(255)]
        public string address { get; set; } = string.Empty;
    }
}
