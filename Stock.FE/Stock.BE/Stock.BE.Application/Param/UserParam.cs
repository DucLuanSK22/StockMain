using System.ComponentModel.DataAnnotations;

namespace Stock.BE.Application.Param
{
    public class UserParam
    {
        /// <summary>
        /// Id người dùng
        /// </summary>
        [Required]
        public Guid user_id { get; set; } = Guid.Empty;
        /// <summary>
        /// Tên đăng nhập của người dùng
        /// </summary>

        public string user_name { get; set; } = string.Empty;
        /// <summary>
        /// Email người dùng
        /// </summary>
        [Required, EmailAddress]
        public string email { get; set; } = string.Empty;

        /// <summary>
        /// Ảnh đại diện của người dùng
        /// </summary>
        public string avatar { get; set; } = string.Empty;

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [MaxLength(10)]
        public string phone { get; set; } = string.Empty;

        /// <summary>
        /// Người dùng có tiềm năng không
        /// </summary>
        public bool? potential { get; set; }
    }

}
