<template>
  <div class="modal">
    <div class="modal-container">
      <slot name="container"></slot>
    </div>
  </div>
</template>

<script>
export default {
  name: "MModal",
  props: {},
  data() {},
  watch: {},

  methods: {},
};
</script>

<style lang="scss">
@import "./modal.scss";
</style>
