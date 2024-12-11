using System.ComponentModel.DataAnnotations;

namespace Stock.BE.Application.Param
{
    public class UserRegisterParam
    {
        /// <summary>
        /// Tên đăng nhập của người dùng
        /// </summary>
        [MinLength(4, ErrorMessage = "Tên người dùng tối thiểu 4 ký tự")]
        [MaxLength(20, ErrorMessage = "Tên đăng nhập người dùng tối đa 20 ký tự")]
        [Required(ErrorMessage = "Tên đăng nhập của người dùng không được để trống")]
        public string fullname { get; set; } = string.Empty;

        /// <summary>
        /// Mật khẩu được mã hóa và lưu vào db.
        /// </summary>
        [MinLength(4, ErrorMessage = "Mật khẩu dùng tối thiểu 4 ký tự")]
        [MaxLength(20, ErrorMessage = "Mật khẩu người dùng tối đa 20 ký tự")]
        [Required(ErrorMessage = "Mật khẩu của người dùng không được để trống")]

        public string password { get; set; } = string.Empty;

        /// <summary>
        /// Email người dùng
        /// </summary>
        [Required, EmailAddress]
        public string email { get; set; } = string.Empty;

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [MaxLength(10)]
        public string phone { get; set; } = string.Empty;

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [MaxLength(255)]
        public string address { get; set; } = string.Empty;

        /// <summary>
        /// Người dùng có tiềm năng không
        /// </summary>
        public bool? potential { get; set; }

    }
}
