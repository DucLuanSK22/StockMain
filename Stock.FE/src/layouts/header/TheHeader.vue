<template>
  <div class="header">
    <div class="header_left">
      <micon type="Logo" />
    </div>
    <div class="header_center">
      <div class="tab_item"><router-link to="/"> <span>Trang chủ</span></router-link></div>
      <div class="tab_item"><router-link to="/purchase"> <span>Đặt lệnh</span></router-link></div>
      <div class="tab_item"><router-link to="/asset"> <span>Tài sản</span></router-link></div>
    </div>
    <div class="header_right">
      <div :style="user && { color: '#0098d3' }">
        <micon type="Avatar" />
      </div>
    </div>
  </div>
</template>

<script>
import { RouterLink } from "vue-router";
export default {
  name: "TheHeader",
  inheritAttrs: false,
  props: { ...RouterLink.props },
  data() {
    return {

    };
  },
  computed: {

  },

  methods: {


  },

  /**
   * Đăng ký sự kiện resize cho widow
   */
  mounted() {

  },
  // beforeUnmount() {
  //   SignalRService.disconnect();
  // }
};
</script>

<style lang="scss">
@import "./header.scss";
</style>
