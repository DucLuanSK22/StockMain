<template>
  <div class="gender-form grid__column-6">
    <div class="input-label-wrapper">
      <div class="input-label">{{ label }}</div>
    </div>
    <div class="flex-start" style="height: 32px">
      <div
        class="flex-start btn-radio"
        v-for="item in options"
        :key="item.value"
      >
        <label class="radio-container">
          <input
            type="radio"
            v-model="radioValue"
            :value="item.value"
            :checked="item.value === radioValue"
            :tabindex="tabIndex"
          />
          <span class="checkmark"></span>
          <span class="focus"></span>
        </label>
        <span style="font-size: 13px">{{ item.label }}</span>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "InputRadio",
  props: {
    modelValue: {
      type: Number,
    },
    options: {
      type: Array,
    },
    tabIndex: {
      type: String,
    },
    label: {
      type: String,
    },
  },
  data() {
    return {
      radioValue: this.modelValue,
    };
  },
  watch: {
    modelValue() {
      this.radioValue = this.modelValue;
    },
    radioValue(newValue) {
      this.$emit("update:modelValue", newValue);
    },
  },
};
</script>

<style lang="scss" scoped>
@import "./radio.scss";
</style>
