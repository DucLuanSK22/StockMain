<template>
  <div class="input-default search" :class="collapse ? 'collapse' : ''">
    <input :placeholder="placeholder" class="input" style="height: 36px" v-model="dataValue" @focus="handleFocus"
      @input="onInput" @keydown="onKeyDown($event)" />
    <div class="input-icon">
      <micon type="Search" v-if="!dataValue" />
      <div class="icon-close-wrapper" v-else @click="onClear">
        <micon type="Close" class="icon_close" />
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
    datas: {
      type: Array,
      default: [],
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
      indexHover: 0,
    };
  },
  watch: {
    modelValue() {
      this.dataValue = this.modelValue;
    },
    datas() {
      this.data = this.datas;
    },
  },

  methods: {
    /**
     * Sự kiện khi người dùng gõ vào input.
     */
    onInput: debounce(function (e) {
      this.setValue("onInput", e.target.value);
    }, 500),
    setValue(eventName, value) {
      this.$emit("update:modelValue", value);
      this.$emit(eventName, value);
    },
    /**
     * Sự kiện blur vào ô input.
     */
    handleFocus() {
      if (this.hasCollapse) {
        this.collapse = false;
      }
    },
    onKeyDown(event) {
      try {
        const keyCode = event.keyCode;
        if (keyCode === 40) {
          if (this.indexHover < this.data.length - 1) {
            this.indexHover++;
          } else {
            this.indexHover = 0;
          }
          this.$emit("indexHover", this.indexHover);
        } else if (keyCode === 38) {
          if (this.indexHover > 0) {
            this.indexHover--;
          } else {
            this.indexHover = this.data.length - 1;
          }
          this.$emit("indexHover", this.indexHover);
        } else if (keyCode === 13) {
          this.setValue("onBlur", this.data[this.indexHover]);

        } else if (keyCode === 9) {
          this.setValue("onBlur", this.data[this.indexHover]);
        }
      } catch (e) {
        console.log(e);
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
