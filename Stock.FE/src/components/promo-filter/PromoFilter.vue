<template>
  <div class="promo-filter">
    <h2 class="promo-filter-title">{{ title }}</h2>
    <div class="promo-filter-list">
      <div
        v-for="item in filterItems"
        :key="item.title"
        class="promo-filter-item"
        @click="selectFilter(item.title)"
      >
        <p>
          {{ item.title }}
        </p>
        <div class="checkbox-wrapper">
          <input
            type="radio"
            :value="item.title"
            v-model="selectedItem"
            class="select"
            @change="emitSelectedFilter"
          />
        </div>
      </div>
     
    </div>
    <div class="promo-filter-clear" v-if="selectedItem">
        <button class="clear-button" @click="clearFilter">Bỏ lọc X</button>
      </div>
  </div>
</template>

<script>
export default {
  name: "PromoFilter",
  props: {
    title: {
      type: String,
      default: "Lọc theo thời gian",
    },
    filterItems: {
      type: Array,
      required: true,
      default: () => [
        { title: "Hôm nay" },
        { title: "Tháng này" },
        { title: "Năm nay" },
      ],
    },
  },
  data() {
    return {
      selectedItem: "",
    };
  },
  methods: {
    selectFilter(title) {
      this.selectedItem = title;
    },
    emitSelectedFilter() {
      this.$emit("selected-filter", this.selectedItem);
    },
    clearFilter() {
      this.selectedItem = ""; // Xóa lựa chọn hiện tại
      this.$emit("selected-filter", ""); // Gửi sự kiện với giá trị rỗng để báo hiệu bỏ lọc
    }
  },
};
</script>

<style lang="scss" scoped>
@import "./promofilter.scss";
</style>
