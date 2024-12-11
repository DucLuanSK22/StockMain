<template>
  <div class="modal" style="z-index: 9999" v-if="isShowMessage">
    <div class="dialog modal-container">
      <div class="dialog-container">
        <div class="ds-body-regular ng-font-neutral-80 ds-medium">
          {{ title }}
        </div>
        <div class="dialog-content flex-start">
          <div class="dialog-icon flex-center">
            <div class="icon-success-medium"></div>
          </div>
          <span class="dialog-desc">{{ content }} </span>
        </div>
        <mseparate />
      </div>
      <div class="wrapper-button flex-center" style="padding: 0">
        <div @click="onClose" class="btn">
          <span>Đồng ý</span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";
export default {
  name: "MDialogMessage",

  methods: {
    //hàm đóng dialog
    onClose() {
      this.$store.commit("dialog/hideMessage");
      if (this.callback) {
        this.callback();
      }
    },
  },
  // sử dụng map state để lấy ra các state của dialog.
  computed: mapState({
    content: (state) => state.dialog.content,
    title: (state) => state.dialog.title,
    callback: (state) => state.dialog.callback,
    isShowMessage: (state) => state.dialog.isShowMessage,
  }),
};
</script>

<style lang="scss" scoped>
@import "./dialog.scss";
.btn {
  background: #0089ff;
  width: fit-content;
  padding: 4px 8px;
  color: #fff;
  font-size: 13px;
  border-radius: 2px;
  cursor: pointer;
  margin: auto;
}
</style>
