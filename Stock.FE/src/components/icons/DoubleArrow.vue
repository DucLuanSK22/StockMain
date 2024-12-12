<template>
  <div class="double_arrow">
    <svg stroke="currentColor" fill="currentColor" stroke-width="0" viewBox="0 0 16 16" size="10" class="" height="10"
      width="10" xmlns="http://www.w3.org/2000/svg">
      <path d="M8 5l-5 6.234h10L8 5z"></path>
    </svg>
    <svg stroke="currentColor" fill="currentColor" stroke-width="0" viewBox="0 0 16 16" size="10" class="" height="10"
      width="10" xmlns="http://www.w3.org/2000/svg">
      <path d="M8 11L3 4.766h10L8 11z"></path>
    </svg>
  </div>
</template>

<script>
export default {
  props: {},
  name: "DoubleArrow",
};
</script>
<style scoped>
.double_arrow {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-self: start;
  position: absolute;
  top: 0;
  right: -4px;
}
</style>
