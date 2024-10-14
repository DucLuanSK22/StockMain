import { format, register } from 'timeago.js';

class CommonFunc {
/**
 * Returns an array of strings based on the given number and index.
 *
 * @param {number} number - The number to be used in the array.
 * @param {number} index - The index of the array to be returned.
 * @return {array} An array of strings based on the given number and index.
 * @author nttue 30.03.2024
 */
 localeFunc(number, index) {
  return [
    ['vừa xong', 'một lúc'],
    ['%s giây trước', 'trong %s giây'],
    ['1 phút trước', 'trong 1 phút'],
    ['%s phút trước', 'trong %s phút'],
    ['1 giờ trước', 'trong 1 giờ'],
    ['%s giờ trước', 'trong %s giờ'],
    ['1 ngày trước', 'trong 1 ngày'],
    ['%s ngày trước', 'trong %s ngày'],
    ['1 tuần trước', 'trong 1 tuần'],
    ['%s tuần trước', 'trong %s tuần'],
    ['1 tháng trước', 'trong 1 tháng'],
    ['%s tháng trước', 'trong %s tháng'],
    ['1 năm trước', 'trong 1 năm'],
    ['%s năm trước', 'trong %s năm'],
  ][index];
}



/**
 * Formats the given time using the 'vi-custom' locale.
 *
 * @param {type} time - the time to be formatted
 * @return {type} the formatted time in 'vi-custom' locale
 * @author nttue 30.03.2024
 */
 formatTime(time) {
  register('vi-custom', this.localeFunc);
  return   format(time, 'vi-custom');
}

/**
 * Uppercase first letter of each word in a string
 * @param {string} value - String to modify
 * @returns {string} String with first letter of each word capitalized
 * @author nttue 30.03.2024
 */
 upperCaseName(value = "") {
  return value
    .split(" ")
    .map((word) => word.charAt(0).toUpperCase() + word.slice(1))
    .join(" ");
}
 convertDate(day) {
  let result = "";
  switch (day) {
    case 1:
      result = "Thứ hai";
      break;
    case 2:
      result = "Thứ ba";
      break;
    case 3:
      result = "Thứ tư";
      break;
    case 4:
      result = "Thứ năm";
      break;
    case 5:
      result = "Thứ sáu";
      break;
    case 7:
      result = "Thứ bảy";
      break;
    default:
      result = "Chủ nhật";
      break;
  }

  return result;
}
/**
 * Builds and returns a text representation of the open time based on the current time and the provided open and close 
 * @author nttue 30.03.2024times.
 *
 * @param {string} timeOpen - The opening time in HH:mm format
 * @param {string} timeClose - The closing time in HH:mm format
 * @return {string} The text representation of the open time
 */
 buildOpenTimeText(timeOpen, timeClose) {
  if (timeOpen && timeClose) {
    const current = new Date();
    const open = new Date(
      current.getFullYear(),
      current.getMonth(),
      current.getDate(),
      timeOpen.split(":")[0],
      timeOpen.split(":")[1]
    );
    const close = new Date(
      current.getFullYear(),
      current.getMonth(),
      current.getDate(),
      timeClose.split(":")[0],
      timeClose.split(":")[1]
    );

    if (current >= open && current <= close) {
      const [house, minutes] = timeClose.split(":");
      return `Đang mở · Đóng cửa lúc ${house}h${minutes}`;
    } else if (current <= open) {
      const [house, minutes] = timeOpen.split(":");
      return `Đóng cửa · Mở cửa lúc ${house}h${minutes}`;
    } else {
      const [house, minutes] = timeOpen.split(":");
      return `Đóng cửa · Mở cửa vào ${house}h${minutes} ${convertDate(
        current.getDay() + 1
      )}`;
    }
  }
  return "";
}

 formatAmount(amount) {
  if (amount) {
    let formattedAmount = amount
      .toString()
      .replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    return formattedAmount;
  } else {
    return "";
  }
}

/**
 * Find the parent element of a given current tag that contains a specific target tag.
 *
 * @param {HTMLElement} currentTag - The current tag to start searching from.
 * @param {string} targetTag - The target tag to search for within the parent elements.
 * @return {boolean} Returns true if the target tag is found in the parent elements, otherwise false.
 * @author nttue 30.03.2024
 */
 handleClickOutSide(currentTag, parentTag, callback) {
  try {
    var tag = currentTag,
    result = false;
    while (!result && tag && tag !== document) {
      if (tag.className && typeof tag.className === 'string' && tag.className?.includes(parentTag)) {
          result = true;
        } else {
          tag = tag.parentElement;
        }
      }
      if (result) {
        return;
      } else {
        callback();  
      }
  } catch (e) {
    console.log(e);
  } 
}

/**
 * Finds the parent element of a given tag that contains a specific class name.
 *
 * @param {HTMLElement} currentTag - The current HTML element to start searching from.
 * @param {string} targetTag - The class name to search for in the parent elements.
 * @return {boolean} Returns true if the parent element with the specified class name is found, otherwise false.
 * @author nttue 30.03.2024
 */
 findParent(currentTag, targetTag, parentTagName) {
    var tag = currentTag,
    result = false;
    while (!result && !tag.className.includes(parentTagName) && tag !== document) {
    if (tag.className.includes(targetTag)) {
      result = true;
    } else {
      tag = currentTag.parentElement;
    }
    }
    return result;
}
}
export default new CommonFunc();
