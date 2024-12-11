/**
 * @param {Funtion} fn hàm thực hiện debounce
 * @param {Number} delay thời gian delay thực hiện hàm
 * @returns sau khoảng thời gian delay trả lại hàm đó
 * @author Nguyễn Trọng Tuế
 */

export default function debounce(fn, delay) {
  var timer = null;
  return function () {
    clearTimeout(timer);
    var args = arguments;
    var that = this;
    timer = setTimeout(function () {
      fn.apply(that, args);
    }, delay);
  };
}
