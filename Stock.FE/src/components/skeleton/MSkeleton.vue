<template>
  <div class="skeleton-single-wrapper" v-if="type === 'single'"></div>
  <div class="skeleton_wrapper" v-else-if="type === 'sidebar-item'">
    <div class="skeleton_wrapper__right">
      <div class="skeleton-single-wrapper"></div>

    </div>
    <div class="skeleton_wrapper__left">
      <div class="skeleton_wrapper__left_content">
        <div class="skeleton-single-wrapper"></div>
        <div class="skeleton-single-wrapper"></div>
      </div>
      <div class="skeleton-single-wrapper"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Skeleton",
  props: {
    type: {
      type: String,
      default: "single",
    }
  }
};
</script>

<style lang="scss">
@import "./skeleton.scss";
</style>
