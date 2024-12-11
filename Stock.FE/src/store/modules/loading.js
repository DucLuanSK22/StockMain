const state = () => ({
  showLoading: false,
});

const mutations = {
  /**
   * Thực hiện show toast message khi thực hiện 1 tác vụ thành công.
   */
  showLoading(state) {
    state.showLoading = true;
  },

  /**
   * Thực hiện show toast message khi thực hiện 1 tác vụ thành công.
   */
  hideLoading(state) {
    state.showLoading = false;
  },
};

const getters = {};

const actions = {};
export default { state, getters, mutations, actions };
