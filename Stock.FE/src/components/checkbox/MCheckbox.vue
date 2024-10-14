<template>
  <label class="checkbox-container">
    <input type="checkbox" :checked="checked" @input="onInput" />
    <span :class="'checkmark'"></span>
    <span class="checkbox-focus"></span>
  </label>
</template>

<script>
export default {
  name: "MCheckbox",
  props: {
    checked: {
      type: Boolean,
      default: true,
    },
  },
  watch: {},
  methods: {
    /**
     * Hàm gửi đi sự kiện input vào checkbox.
     */
    onInput() {
      this.$emit("onInput");
    },
  },
};
</script>

<style scoped lang="scss">
@import "./checkbox.scss";
</style>
