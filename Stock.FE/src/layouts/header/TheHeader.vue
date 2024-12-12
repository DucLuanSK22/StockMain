<template>
  <div class="header">
    <!-- <div class="header_left">
      <micon type="Logo" />
    </div> -->
    <div class="header_center">
      <div class="tab_item"><router-link to="/"> <span>Trang chủ</span></router-link></div>
      <div class="tab_item"><router-link to="/purchase"> <span>Đặt lệnh</span></router-link></div>
      <div class="tab_item"><router-link to="/asset"> <span>Tài sản</span></router-link></div>
      <div class="tab_item"><router-link to="/report"> <span>Báo cáo</span></router-link></div>
    </div>
    <Clock />
    <div class="header_right pointer">
      <div :style="user && { color: '#0098d3' }" @click="showPopupAvatar = !showPopupAvatar">
        <micon type="Avatar" />
      </div>

      <div class="popup-avatar" v-if="showPopupAvatar" v-click-outside="handleClickOutSide">
        <div class="title mb-2 mt-1">
          Xin chào, {{ user?.user_name }}
        </div>
        <mbutton button-text="Đăng xuất" @click="handleLogout" class="none-background" />
      </div>
    </div>
  </div>
</template>

<script>
import { RouterLink } from "vue-router";
import Clock from "@/components/clock/Clock.vue";
export default {
  name: "TheHeader",
  components: {
    Clock
  },
  inheritAttrs: false,
  props: { ...RouterLink.props },
  data() {
    return {
      showPopupAvatar: false,
      user: {},
    };
  },
  computed: {

  },
  mounted() {
    const user = this.$ms.cache.getCache("user");
    if (user) {
      this.user = user;
    }

  },
  methods: {
    handleClickOutSide() {

    },
    async handleLogout() {
      try {

        this.$store.commit("showLoading");
        this.$ms.cache.deleteCache("user");
        await new Promise((resovle) =>
          setTimeout(resovle, 500));
        this.user = null;
        this.showPopupAvatar = false;
        this.$router.push({
          path: "/login",
        });
        this.$store.commit("hideLoading");
      } catch (e) {
        this.$store.commit("hideLoading");
        console.log(e);
      }
    },

  },

};
</script>

<style lang="scss">
@import "./header.scss";
</style>
