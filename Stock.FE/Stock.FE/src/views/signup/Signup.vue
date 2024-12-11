<template>
  <div class="login">

    <div class="login__left">
      <div class="login__left__top">
        <div class="title">ĐĂNG KÝ</div>
        <div class="sub_title">
          <span class="no__account">Trải nghiệm khi chưa có tài khoản?</span>
          <span class="login__text" @click="handleDirectLogin">Đăng nhập</span>
        </div>

      </div>
      <div class="login__left__center">
        <minput label="Nhập tên tài khoản" name="Username" v-model="objectMaster.user_name" ref="Username"
          rules="required|maxlength_100" formName="User" class="mb-2 row-100" autocomplete="off" />

        <minput label="Nhập email" name="Email" v-model="objectMaster.email" ref="Email"
          placeholder-input="abc@gmail.com" rules="required|maxlength_100" formName="User" class="mb-2 row-100"
          autocomplete="off" />

        <minput label="Nhập mật khẩu" name="Password" v-model="objectMaster.password" ref="Password"
          rules="required|maxlength_100" typeInput="password" formName="User" class="mb-2 row-100" autocomplete="off"
          :icon1="icon2" :icon2="icon1" :click-icon="handleSwitchVisibility" />

      </div>
      <div class="login__bottom__bottom">
        <mbutton buttonText="Đăng ký" @click="handleSubmit" />
      </div>
    </div>
  </div>
</template>

<script>

import StockAPI from "@/apis/StockAPI";
import Eye from "@/components/icons/Eye.vue";
import HideEye from "@/components/icons/HideEye.vue";
import validateMixin from "@/mixins/validateMixin.vue";
import enterFormMixin from "@/mixins/enterFormMixin.vue";
export default {
  name: "Signup",
  mixins: [validateMixin, enterFormMixin],
  data() {
    return {
      objectMaster: {},
      icon1: HideEye,
      icon2: Eye,
    };

  },

  async mounted() {

  },
  methods: {
    /**
    * Hàm xử lý sự kiện đăng nhập của người dùng
    * @author nttue - 30.09.2024
    */
    async handleSubmit() {
      try {
        this.$store.commit("showLoading");
        await StockAPI.register(this.objectMaster);
        this.$store.commit("showToast", {
          label: "Đăng ký tài khoản thành công.",
          type: 'success'
        });
        this.$store.commit("hideLoading")
        this.$router.push({
          path: "/login",
        });
      } catch (error) {
        console.log(error);
        this.$store.commit("showToast", {
          label: "Đăng ký tài khoản thất bại.",
          type: 'error'
        });
        this.$store.commit("hideLoading")
      }

    },
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
