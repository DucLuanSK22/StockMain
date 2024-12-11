<template>
  <div class="login">
    <div class="login__right">
      <div class="login__right__background">
        <micon type="Background" />
      </div>
      <div class="login__right__content">
        <micon type="Quote" />
        <span>"It's not whether you're right or wrong that's important, but how much money you make when you're right
          and how much you lose when you're wrong."</span>
        <div class="horizontal">
        </div>
        <div class="author">
          George Soros</div>
      </div>
    </div>
    <div class="login__left">
      <div class="login__left__top">
        <div class="title">ĐĂNG KÝ</div>
        <div class="sub_title">
          <span class="no__account">Trải nghiệm khi chưa có tài khoản?</span>
          <span class="login__text" @click="handleDirectLogin">Đăng ký ngay</span>
        </div>

      </div>
      <div class="login__left__center">
        <minput label="Nhập email Đăng ký" name="Email" v-model="objectMaster.email" ref="Email"
          placeholder-input="abc@gmail.com" rules="required|maxlength_100" formName="Booking" class="mb-2 row-100" />

        <minput label="Tạo mật khẩu" name="Password" v-model="objectMaster.password" ref="Password"
          rules="required|maxlength_100" formName="Booking" class="mb-2 row-100" />

      </div>
      <div class="login__bottom__bottom">
        <mbutton buttonText="Đăng nhập" type="link" href="/login" />
      </div>
    </div>
  </div>
</template>

<script>


export default {
  name: "Signup",
  components: {

  },
  data() {
    return { objectMaster: {} };
  },

  async mounted() {

  },

  methods: {
    handleDirectLogin() {
      this.$router.push({
        path: "/login",
      });
    }

  },
};
</script>

<style lang="scss">
@import "./signup.scss";
</style>
