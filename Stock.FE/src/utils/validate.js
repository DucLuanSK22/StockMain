/**
 * @description
 * hàm trả về một số quy tác validate dữ liệu
 * @author Nguyễn Trọng Tuế
 */

export const validate = {
  /**
   * Trường yêu cầu bắt buộc
   * @param {*} value giá trị của input
   * @param {*} message thống báo lỗi tương ứng
   * @returns Trả về undefined nếu hợp lệ ngược lại trả lại lỗi
   */
  required(value, message) {
    return value ? undefined : message;
  },
  /**
   * Kiểm tra định dạng code
   */
  employeeCode(value, message) {
    var subCode = value.slice(0, 3).toUpperCase();
    return subCode != "NV-" ? message : undefined;
  },
  /**
   * Kiểm tra định dạng email
   */
  email(value, message) {
    var regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    return regex.test(value) && value ? undefined : message;
  },
  /**
   * Kiểm tra định dạng là số
   */
  number(value, message) {
    var regex = /^[0-9]+$/;
    return !value || regex.test(value) ? undefined : message;
  },

  /**
   * Hàm kiểm tra xem ngày được chọn có lơn hơn ngày hiện tại không.
   * @param {Date} value Giá trị ngày tháng cần kiểm tra.
   * @param {String} message Thông báo lỗi tương ứng.
   * @returns Trả về thông báo lỗi nếu có, ngược lại undifined.
   */
  date(value, message) {
    var date = new Date();
    return value < date ? undefined : message;
  },

  /**
   * Hàm kiểm tra xem độ dài 1 chuỗi có lơn 10 ký tự không.
   * @param {String} value Giá trị cần kiểm
   * @param {String} message Thông báo lỗi tương ứng.
   * @returns Trả về thông báo lỗi nếu có, ngược lại undifined.
   */
  maxlength_10(value, message) {
    return !value || value.length <= 10 ? undefined : message;
  },

  /**
   * Hàm kiểm tra xem độ dài 1 chuỗi có lơn hơn hoặc bằng 100 ký tự không.
   * @param {String} value Giá trị cần kiểm
   * @param {String} message Thông báo lỗi tương ứng.
   * @returns Trả về thông báo lỗi nếu có, ngược lại undifined.
   */
  maxlength_100(value, message) {
    return value && value.length > 100 ? message : undefined;
  },
  /**
   * Hàm kiểm tra xem độ dài 1 chuỗi có ngắn hơn 8 ký tự không.
   * @param {String} value Giá trị cần kiểm
   * @param {String} message Thông báo lỗi tương ứng.
   * @returns Trả về thông báo lỗi nếu có, ngược lại undifined.
   */
  minlength_8(value, message) {
    return !value || value.length >= 8 ? undefined : message;
  },
  /**
   * Kiểm tra xem chuỗi có chứa ký tự in hoa không.
   *
   * @param {string} str - chuỗi cần kiểm tra
   * @return {boolean} true nếu chuỗi chứa ít nhất một ký tự in hoa, ngược lại trả về false
   */
  containsUppercase(str, message) {
    const regex = /[A-Z]/;
    return str && regex.test(str) ? undefined : message;
  },
  /**
   * Kiểm tra xem chuỗi có chứa ký tự đặc biệt không.
   *
   * @param {string} str - chuỗi cần kiểm tra
   * @return {boolean} true nếu chuỗi chứa ít nhất một ký tự đặc biệt, ngược lại trả về false
   */
  containsSpecialCharacter(str, message) {
    const regex = /[&$!%@]/; // Thay thế [&$!%] bằng bất kỳ ký tự đặc biệt nào bạn muốn kiểm tra
    return str && regex.test(str) ? undefined : message;
  },
  /**
   * Kiểm tra xem giá trị có chứa số không.
   *
   * @param {string} value - giá trị cần kiểm tra
   * @param {string} message - thông báo trả về nếu giá trị chứa số
   * @return {string|undefined} thông báo nếu giá trị chứa số, nếu không trả về undefined
   */
  includeNumber(value, message) {
    const regex = /[0-9]/;
    return value && value.split("").some((char) => regex.test(char))
      ? undefined
      : message;
  },
};
