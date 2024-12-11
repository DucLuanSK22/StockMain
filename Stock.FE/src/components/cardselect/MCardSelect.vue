<template>
  <div class="card-wrapper">
    <div class="card-left">
      <div class="service-name">
        <span class="service-text">{{ item.service_code }}</span>
        <span class="service-icon"><micon type="Info" /></span>
      </div>
      <div class="keyword">
        <span class="keyword-icon"><micon type="Tag" /></span>
        <span class="keyword-text">{{ item.service_code }} </span>
      </div>
    </div>
    <div class="card-right">
      <div class="checkbox-wrapper">
        <input type="checkbox" v-model="item.value" class="select" />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MCardSelect",
  props: {
    item: {
      type: Object,
      default: null,
    },
  },
  data() {},
  watch: {},

  methods: {},
};
</script>

<style lang="scss">
@import "./cardselect.scss";
</style>
