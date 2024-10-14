<template>
  <div class="filter-gara">
    <div class="filter-gara__body">
      <div class="filter-gara__item flex-start" v-for="(item, index) in data" :key="index">
        <div class="filter-gara__item-right flex-center">
          <input v-if="choose == 'radio'" type="radio" v-model="radioValue" :value="item.value"
            :checked="item.value === radioValue" />

          <input v-else type="checkbox" v-model="item.value" />
        </div>
        <div class="filter-gara__item-left">{{ item.text }}</div>
      </div>
    </div>
    <div class="filter-gara__bottom flex-between">
      <div class="filter-gara__bottom-left" @click="clearOptions">Xóa</div>
      <div class="filter-gara__bottom-right" @click="saveOptions">Lưu</div>
    </div>
  </div>
</template>

<script>
import MCheckbox from "@/components/checkbox/MCheckbox.vue";
export default {
  name: "MFilter",
  emits: ["saveOptions", "clearChecked"],
  components: {
    MCheckbox,
  },
  props: {
    text: {
      type: String,
      default: "Sắp xếp theo",
    },
    modelValue: {
      type: [String, Number],
      default: "",
    },
    data: {
      type: Array,
      default: [],
    },
    chooseType: {
      type: String,
      default: "",
    },
    filterId: {
      type: String,
      default: "",
    },
    index: {
      type: Number,
      default: -1,
    },
  },
  data() {
    return {
      radioValue: this.modelValue,
      choose: this.chooseType,
    };
  },

  watch: {
    radioValue(newValue) {
      if (this.chooseType != "checkbox") {
        this.$emit("update:modelValue", newValue);
      }
    },

    modelValue(newValue) {
      if (this.chooseType != "checkbox") {
        this.radioValue = newValue;
      }
    },
  },

  methods: {
    clearOptions() {
      if (this.chooseType != "checkbox") {
        this.$emit("saveOptions", this.filterId, "", this.index);
      } else {
        this.$emit("clearChecked", this.index);
      }
    },
    saveOptions() {
      this.$emit("saveOptions", this.filterId, this.radioValue, this.index);
    },
  },
};
</script>

<style lang="scss">
@import "./filter.scss";
</style>
