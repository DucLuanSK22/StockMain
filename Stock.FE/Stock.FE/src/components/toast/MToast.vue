<template>
  <div id="toast" v-if="isShowToast">
    <div :class="content.type == 'error' ? 'toast error' : 'toast success'">
      <div :class="content.type == 'error' ? 'icon-warning-red' : 'icon-success-medium'
    "></div>
      <span class="toast-message"> {{ content.label }}</span>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "MToast",
  props: {
    label: {
      label: String,
    },
  },
  /**
   * Sử dụng computed để lấy ra các state global
   */
  computed: mapState({
    content: (state) => state.toast.content,
    type: (state) => state.toast.type,
    isShowToast: (state) => state.toast.isShowToast,
  }),
};
</script>

<style lang="scss">
@import "./toast.scss";
</style>
