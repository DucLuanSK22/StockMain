<!-- MyComponent.vue -->
<template>
  
  <StarRating 
    v-model="myRating" 
    :rating="myRating"
    :fixed-points="2"
    :read-only="disableClick"
    :star-size="size"
    :show-rating="false"
    active-color="#e98b2f"
    @update:rating ="setRating"
    :border-width="1"
    :round-start-rating="false"
  ></StarRating> 
</template>

<script>
import StarRating from 'vue-star-rating';
export default {
  components: {
  StarRating
},
  props: {
    rating: {
      type: [Number, String],
      default: 4
    },
    disableClick: {
      type: Boolean,
      default: true,
    },
    size: {
      type: Number,
      default: 18
    }
  },
  data() {
    return {
      myRating: this.rating,
    };
  },

  watch: {
    rating(newVaue) {
      this.myRating = newVaue;
    },
    
  },
  methods: {
    setRating(rating) {
       this.$emit('update', rating);
    }
  }
};
</script>
