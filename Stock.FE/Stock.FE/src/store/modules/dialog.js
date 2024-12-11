const state = {
  isShowMessage: false,
  content: "",
  callback: null,
  title: "",
};

const mutations = {
  /**
   * Hàm thực hiện nhận nội dung thông báo và hiển thị lỗi lên dialog.
   * @param {Object} state
   * @param {String} message Thông báo lỗi
   */
  showMessage(state, obj) {
    state.isShowMessage = true;
    state.content = obj.content;
    state.callback = obj.callback;
    state.title = obj.title;
  },
  /**
   * Hàm thực hiện ẩn dialog khi người dùng đóng dialog.
   */
  hideMessage(state) {
    state.isShowMessage = false;
  },
};

const actions = {};

const getters = {};

export default {
  namespaced: true,
  state,
  mutations,
  actions,
  getters,
};
