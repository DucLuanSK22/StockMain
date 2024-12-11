<template>
  <div class="asset">
    <div class="asset__left">
      <div class="home__right_search">
        <msearch v-model="search" :placeholder="'Tìm kiếm mã'"></msearch>
      </div>
      <div class="home__right__bottom">
        <div class="home__right_title">
          <span>Mã</span>
        </div>
        <div class="home__right_table">

          <ctable />
        </div>
      </div>
    </div>
    <div class="asset__right">

      <Doughnut />
    </div>
  </div>
</template>

<script>
import Doughnut from '@/components/doughnut/Doughnut.vue';
export default {
  components: {
    Doughnut
  },
  data() {
    return {

    };
  },

  async mounted() {

  },

  methods: {},
};
</script>

<style lang="scss" scoped>
@import "./asset.scss";
</style>
