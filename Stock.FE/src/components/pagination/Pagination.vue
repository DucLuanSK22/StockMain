<template>
  <div class="panigation mt-2 mb-2 flex-between">
    <div class="panigation__left" @click="prevPage" :class="disablePrevBtn && 'disable'">
      <span>Trước</span>
    </div>
    <div class="panigation__center flex-center">
      <div class="panigation__center-item" :class="pageNumber === 0 && 'current-page'" @click="handleSelectedPage(0)">
        <span>1</span>
      </div>
      <div v-show="(pageNumber >= 0 && pageNumber < 2 && lastPageNumber > 1) || lastPageNumber === 2 || lastPageNumber === 1
      " class="group-select">
        <div class="panigation__center-item" :class="pageNumber === 1 && 'current-page'" @click="handleSelectedPage(1)">
          <span>{{ 2 }}</span>
        </div>
      </div>

      <div class="group-select" v-show="pageNumber >= 2 &&
      pageNumber <= lastPageNumber - 2 &&
      lastPageNumber !== 2
      ">
        <div class="pointer">
          <div @click="onReducePage" class="panigation__center-item">...</div>
        </div>

        <div class="panigation__center-item current-page" @click="handleSelectedPage(pageNumber)">
          <span>{{ pageNumber + 1 }}</span>
        </div>
      </div>

      <div v-show="lastPageNumber > 2" @click="onIncreasePage" class="pointer">
        <div class="panigation__center-item">...</div>
      </div>

      <div class="group-select" v-show="pageNumber >= lastPageNumber - 1 &&
      pageNumber <= lastPageNumber + 1 &&
      lastPageNumber >= 3
      ">
        <div class="panigation__center-item" :class="pageNumber === lastPageNumber - 1 && 'current-page'"
          @click="handleSelectedPage(lastPageNumber - 1)">
          <span>{{ lastPageNumber }}</span>
        </div>
      </div>

      <div v-show="length / pageSize > 2" class="panigation__center-item"
        :class="pageNumber === lastPageNumber && 'current-page'" @click="handleSelectedPage(lastPageNumber)">
        <span>{{ lastPageNumber + 1 }}</span>
      </div>
    </div>
    <div class="panigation__right" @click="nextPage" :class="disableNextBtn && 'disable'">
      <span>Sau</span>
    </div>
  </div>
</template>

<script>
export default {
  name: "Pagination",
  props: {
    currentPage: {
      type: Number,
      default: 0,
    },
    length: {
      type: Number,
      default: 0,
    },
    pageSize: {
      type: Number,
      default: 5,
    },
  },
  data() {
    return {
      pageNumber: this.currentPage,
    };
  },
  watch: {},
  computed: {
    disablePrevBtn() {
      return this.pageNumber === 0;
    },
    disableNextBtn() {
      return this.pageNumber + 1 >= this.length / this.pageSize;
    },
    lastPageNumber() {
      let value = this.length / this.pageSize;

      return value % 1 === 0 ? Math.floor(value) - 1 : Math.floor(value);
    },
  },
  methods: {
    /**
     * Giảm vị trị page hiện tại xuống 1.
     */
    prevPage() {
      let value = this.pageNumber - 1 >= 0 ? this.pageNumber - 1 : 0;

      this.handleSelectedPage(value);
    },
    /**
     * Tăng vị trị page hiện tại lên 1.
     */
    nextPage() {
      let value =
        this.pageNumber + 1 < this.lastPageNumber
          ? this.pageNumber + 1
          : this.lastPageNumber;

      this.handleSelectedPage(value);
    },

    /**
     * Chọn page Number bất kỳ.
     * @param {} value Giá trị của page chọn.
     */
    handleSelectedPage(value) {
      this.pageNumber = value;
      this.$emit("handleChangePage", value);
    },

    /**
     * Tăng vị trị page hiện tại lên 1.
     */
    onIncreasePage() {
      let value =
        this.pageNumber + 1 >= this.lastPageNumber
          ? this.lastPageNumber - 1
          : this.pageNumber + 1;

      this.handleSelectedPage(value);
    },

    /**
     * Giảm vị trị page hiện tại xuống 1.
     */
    onReducePage() {
      let value = this.pageNumber - 1 < 1 ? 1 : this.pageNumber - 1;

      this.handleSelectedPage(value);
    },
  },
};
</script>

<style lang="scss" scoped>
@import "./pagination.scss";
</style>
