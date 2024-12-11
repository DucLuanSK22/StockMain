using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.BE.Core.Model
{
    /// <summary>
    /// Bảng chứa thông tin người dùng
    /// </summary>
    [Table("user")]
    public class UserEntity
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
        /// PasswordHash là một mảng byte chứa giá trị băm (hash) của mật khẩu người dùng.
        /// </summary>
        public byte[]? password_hash { get; set; }

        /// <summary>
        /// PasswordSalt là một mảng byte chứa giá trị muối được sử dụng trong quá trình băm mật khẩu.
        /// </summary> 
        public byte[]? password_salt { get; set; }
    }
}
