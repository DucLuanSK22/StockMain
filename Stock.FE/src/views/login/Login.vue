<template>
  <div class="login">
    <div class="login__left">
      <div class="login__left__top">
        <div class="title">ĐĂNG NHẬP</div>
        <div class="sub_title">
          <span class="no__account">Bạn chưa có tài khoản</span>
          <span class="login__text" @click="handleDirectSignup">Đăng nhập</span>
        </div>

      </div>
      <div class="login__left__center">
        <minput label="Nhập email" name="Email" v-model="objectMaster.email" ref="Email" placeholder-input="Tên"
          rules="required|maxlength_100" formName="User" class="mb-2 row-100" />

        <minput label="Tạo mật khẩu" name="Password" v-model="objectMaster.password" ref="Password"
          rules="required|maxlength_100" formName="User" class="mb-2 row-100" typeInput="password" />

      </div>
      <div class="login__bottom__bottom">
        <mbutton buttonText="Đăng nhập" @click="handleLogin" />
      </div>
    </div>
  </div>
</template>

<script>

import StockAPI from "@/apis/StockAPI";
export default {
  name: "Login",
  components: {

  },
  data() {
    return { objectMaster: {} };
  },


  async mounted() {

  },

  methods: {
    /**
     * Hàm xử lý sự kiện đăng nhập của người dùng
     * @author nttue - 30.09.2024
     */
    async handleLogin() {
      try {
        this.$store.commit("showLoading");
        var result = await StockAPI.login(this.objectMaster);
        this.$ms.cache.setCache("user", result);
        this.$store.commit("showToast", {
          label: "Đăng nhập thành công.",
          type: 'success'
        });
        this.$store.commit("hideLoading")
        this.$router.push({
          path: "/",
        });
      } catch (error) {
        console.log(error);
        this.$store.commit("showToast", {
          label: "Đăng nhập thất bại.",
          type: 'error'
        });
        this.$store.commit("hideLoading")
      }

    },
    handleDirectSignup() {
      this.$router.push({
        path: "/signup",
      });
    }
  },
};
</script>

<style lang="scss">
@import "./login.scss";
</style>
