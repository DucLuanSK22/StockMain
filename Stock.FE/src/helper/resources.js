const vi = {
  User: {
    Username: {
      required: "Họ tên không được phép để trống",
      maxlength_100: "Họ tên không được phép vượt quá 100 ký tự",
    },
    Password: {
      required: "Mật khẩu không được phép để trống",
      minlength_8: "Mật khẩu cần dài ít nhất 8 ký tự",
      containsUppercase:
        "Phải có ít nhất một ký tự viết thường và một ký tự viết hoa",
      includeNumber: "Sử dụng ít nhất một số",
      containsSpecialCharacter:
        "Phải sử dụng ít nhất một ký hiệu (&, $, !, %, v.v.)",
    },

    Email: {
      required: "Email không được phép để trống",
      email: "Email không đúng định dạng",
    },

    Phone: {
      number: "Số điện thoại chỉ được phép chứa các ký tự số",
      maxlength_10: "Số điện thoại không được phép vượt quá 10 ký tự",
    },
  },
  Booking: {
    Comment: {
      required: "Vui lý nhập ghi chú",
    },
    FirstName: {
      required: "Vui lòng nhập họ và tên đệm",
      maxlength_100: "Họ và tên đệm không được phép vượt quá 100 ký tự",
    },
    LastName: {
      required: "Vui lòng nhập tên",
      maxlength_100: "Tên không được phép vượt quá 100 ký tự",
    },
    Email: {
      required: "Vui lòng nhập Email",
      email: "Email không đúng định dạng",
    },
    Phone: {
      number: "Số điện thoại chỉ được phép chứa các ký tự số",
      maxlength_10: "Số điện thoại không được phép vượt quá 10 ký tự",
    },
  },
};

export default vi;
