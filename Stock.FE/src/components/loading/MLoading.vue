<template>
  <div class="modal" v-if="showLoading">
    <div class="loader_wrapper">
      <div class="loader"></div>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";
export default {
  name: "MLoading",
  props: {},
  data() { },
  watch: {},
  /**
   * Sử dụng computed để lấy ra các state global
   */
  computed: mapState({
    showLoading: (state) => state.loading.showLoading,
  }),
  methods: {},
};
</script>

<style lang="scss">
@import "./loading.scss";
</style>
