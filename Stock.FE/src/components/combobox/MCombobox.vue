<template>
  <div class="select">
    <p class="combobox-label">{{ label }}</p>
    <div class="combobox flex-between" v-click-outside="handleClickOutSide" :class="{
      'has_border': hasBorder
    }" @click="handleShowDropdown">
      <div class="combobox-selected" :class="{
      'combobox-selected__empty': comboboxValue == ''
    }">
        {{ comboboxValue == "" ? defaultText : comboboxValue }}
      </div>
      <div class="arrow-down pointer">
        <micon type="ChevronDown" v-if="!showDropDown" />
        <micon type="ChevronTop" v-else />
      </div>

      <div class="combobox-dropdown" v-show="showDropDown">
        <div class="combobox-item d-flex" :class="{
      'item-selected': item == comboboxValue,
    }" @click="handleChooseValue(item)" v-for="(item, index) in items" :key="index">
          {{ item }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MCombobox",
  props: {
    disabled: {
      type: Boolean,
      default: false,
    },
    label: {
      type: String,
      default: "",
    },
    defaultText: {
      type: String,
      default: "",
    },
    modelValue: {
      type: String,
      default: new Date(),
    },
    items: {
      type: Array,
      default: [],
    },
    hasBorder: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      comboboxValue: "",
      showDropDown: false,
      chooseValue: false,
    };
  },
  watch: {
    /**
     * Hàm update giá trị của date.
     */
    modelValue() {
      this.comboboxValue = this.modelValue;
    },
  },

  methods: {
    handleChooseValue(item) {
      this.comboboxValue = item;
      this.$emit("update:modelValue", item);
      this.showDropDown = false;
      this.chooseValue = true;
    },
    handleClickOutSide() {
      this.showDropDown = false;
    },
    handleShowDropdown() {
      if (!this.disabled) {
        if (!this.chooseValue) {
          this.showDropDown = !this.showDropDown;
        } else {
          this.chooseValue = false;
        }
      }
    },
  },
};
</script>

<style lang="scss" scoped>
@import "./combobox.scss";
</style>
