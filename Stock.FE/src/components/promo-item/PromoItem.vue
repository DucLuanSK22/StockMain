<template>
  <div class="promo-item-wrapper" @click="handleClick">
    <div class="promo-img">
      <img :src="src" alt="" />
    </div>
    <div class="promo-date">
      <p>{{ date }}</p>
    </div>
    <div class="promo-title">
      <p>{{ title }}</p>
    </div>
    <div class="promo-more">
      <button>Xem thêm ></button>
    </div>
  </div>
</template>

<script>
export default {
  name: "PromoItem",
  props: {
    src: {
      type: String,
      required: true,
    },
    title: {
      type: String,
      required: true,
    },
    date: {
      type: String,
      required: true,
    },
  },
  data() {
    return {};
  },
  watch: {},
  methods: {
    handleClick() {
      this.$emit("click-item");
    }
  },
};
</script>

<style lang="scss">
@import "./promoitem.scss";
</style>
