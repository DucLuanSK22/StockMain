using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Stock.BE.Application.Param
{
    public class UserEditParam
    {
        [MinLength(4, ErrorMessage = "Tên người dùng tối thiểu 4 ký tự")]
        [MaxLength(20, ErrorMessage = "Tên đăng nhập người dùng tối đa 20 ký tự")]
        public string? user_name { get; set; }
        /// <summary>
        /// Email người dùng
        /// </summary>
        [Required, EmailAddress]
        public string email { get; set; } = string.Empty;
        /// <summary>
        /// File ảnh đại diện của người dùng.
        /// </summary>
        public IFormFile? avatar { get; set; }

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
