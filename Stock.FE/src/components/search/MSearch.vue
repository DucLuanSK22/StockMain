<template>
  <div class="input-default search" :class="collapse ? 'collapse' : ''">
    <input :placeholder="placeholder" class="input" style="height: 36px" v-model="dataValue" @focus="handleFocus"
      @blur="handleBlur" @input="onInput" />
    <div class="input-icon">
      <micon type="Search" v-if="!dataValue" />
      <div class="icon-close-wrapper" v-else @click="onClear">
        <micon type="Close" class="icon_close"/>
      </div>
    </div>
  </div>
</template>

<script>
import debounce from "@/utils/debounce.js";
export default {
  name: "InputSearch",
  props: {
    hasCollapse: {
      type: Boolean,
    },
    modelValue: {
      type: String,
      default: "",
    },
    placeholder: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      collapse: this.hasCollapse || false,
      dataValue: this.modelValue,
    };
  },
  watch: {
    modelValue() {
      this.dataValue = this.modelValue;
    },
  },

  methods: {
    /**
     * Sự kiện khi người dùng gõ vào input.
     */
    onInput: debounce(function (e) {
      this.$emit("update:modelValue", e.target.value);
    }, 500),

    /**
     * Sự kiện blur vào ô input.
     */
    handleFocus() {
      if (this.hasCollapse) {
        this.collapse = false;
      }
    },
    /**
     * Sự kiện blur ra ngoài
     */
    handleBlur() {
      if (this.hasCollapse) {
        this.collapse = true;
      }
    },
    /**
     * Sự kiện clear input.
     */
    onClear() {
      this.dataValue = "";
      this.$emit("update:modelValue", this.dataValue);
    },
  },
};
</script>

<style lang="scss">
@import "@/components/input/input.scss";

@import "./search.scss";
</style>
