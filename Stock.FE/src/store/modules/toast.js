const state = () => ({
  isShowToast: false,
  content: {
    label: "",
    type: "",
  },
});

const mutations = {
  /**
   * Thực hiện show toast message khi thực hiện 1 tác vụ thành công.
   */
  showToast(state, payload) {
    state.isShowToast = true;
    state.content = { ...state.content, ...payload };
    setTimeout(() => {
      state.isShowToast = false;
    }, 4000);
  },
};

const getters = {};

const actions = {};
export default { state, getters, mutations, actions };
