/**
 * Hàm xử lí sự kiện khi người dùng click ra ngoài element
 * @author: nttue (20/07/2023)
 */
export const clickOutsideDirective = {
  mounted(el, binding) {
    const onClickOutside = (event) => {
      if (!(el === event.target || el.contains(event.target) )) {
        binding.value();
      }
    };

    document.addEventListener("click", onClickOutside);

    el._clickOutside = onClickOutside;
  },
  beforeUnmount(el) {
    document.removeEventListener("click", el._clickOutside);
    delete el._clickOutside;
  },
};
