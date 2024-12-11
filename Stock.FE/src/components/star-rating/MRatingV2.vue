<template>
  <div class="rate">
    <div :style="{ color: item <= rating ? 'orange' : '' }" class="star" v-for="(item, index) in listStars" :key="index"
      @click="handleRating(item)">
      <micon type="StarsV2" width="40" height="40" />
    </div>
  </div>
</template>

<script>
export default {
  name: "MRatingV2",
  emits: ['updateRating'],
  props: {},
  data() {
    return {
      listStars: [1, 2, 3, 4, 5],
      rating: 0,
    };
  },
  methods: {
    handleRating(item) {
      this.rating = item;
      this.$emit('updateRating', this.rating)
    },
  },
};
</script>

<style lang="scss">
.rate {
  display: flex;

  .star {
    display: flex;
  }
}
</style>
