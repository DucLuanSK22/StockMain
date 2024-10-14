<template>
  <div class="back_to_top" ref="backToTop" @click="scrollToTop">

    <img src="@/assets/img/arrow.png" alt="icon" />
  </div>


</template>

<script>
import scrollDocumentMixin from "@/mixins/scrollDocumentMixin.vue";
export default {
  name: "BackToTop",
  mixins: [scrollDocumentMixin],
  props: {},
  data() {
    return {}
  },
  watch: {},
  // mounted() {
  //   window.addEventListener("scroll", this.handleScroll);
  // },
  // beforeUnmount() {
  //   window.removeEventListener("scroll", this.handleScroll);
  // },
  methods: {

    scrollToTop() {
      const ele = document.querySelector('.container');
      ele.scrollTo({
        top: 0,
        left: 0,
        behavior: 'smooth'
      })

    },

    // handleScroll() {
    //   console.log(1234);
    //   if (document.body.scrollTop > 300 || document.documentElement.scrollTop > 300) {
    //     this.$refs['backToTop'].classList.add("active");
    //   } else {
    //     this.$refs['backToTop'].classList.remove("active");
    //   }
    // }

  },
}; </script>

<style lang="scss">
@import "./back-to-top.scss";
</style>
