<template>
  <div>
    <TheHeader v-if="showHeader"></TheHeader>
    <div :class="showHeader ? 'container has-header' : 'container'">
      <slot> </slot>
    </div>
  </div>
</template>

<script>
import TheHeader from "./header/TheHeader.vue";

export default {
  name: "DefaultLayout",
  props: { showHeader: { type: Boolean, default: true } },
  components: {
    TheHeader,
  },
};
</script>

<style>
.container {
  width: 100%;
  height: calc(100vh);
}

.container.has-header {
  display: flex;
  height: calc(100vh - var(--header-height) - 1px);
  overflow-y: auto;
  
}
</style>
