<template>
  <div class="login">
    <div class="login__right">
      <div class="login__right__background">
        <micon type="Background" />
      </div>
      <div class="login__right__content">
        <micon type="Quote" />
        <span>"Know what you own, and know why you own it."</span>
        <div class="horizontal">
        </div>
        <div class="author">
          Peter Lynch</div>
      </div>
    </div>
    <div class="login__left">
      <div class="login__left__top">
        <div class="title">ĐĂNG NHẬP</div>
        <div class="sub_title">
          <span class="no__account">Bạn chưa có tài khoản</span>
          <span class="login__text" @click="handleDirectSignup">Đăng nhập</span>
        </div>

      </div>
      <div class="login__left__center">
        <minput label="Nhập email Đăng ký" placeholderInput="abc@gmail.com" name="Email" v-model="objectMaster.email"
          ref="Email" placeholder-input="Tên" rules="required|maxlength_100" formName="Booking" class="mb-2 row-100" />

        <minput label="Tạo mật khẩu" name="Password" v-model="objectMaster.password" ref="Password"
          placeholder-input="Mật khẩu" rules="required|maxlength_100" formName="Booking" class="mb-2 row-100" />

      </div>
      <div class="login__bottom__bottom">
        <mbutton buttonText="Đăng nhập" type="link" href="/login" />
      </div>
    </div>
  </div>
</template>

<script>


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
