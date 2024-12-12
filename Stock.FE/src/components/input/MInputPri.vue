<template>
  <div class="input-default" :class="error ? 'error' : ''">


    <div class="input-wrapper">
      <component :is="typeComponent" class="input" ref="minput" :type="typeInput ? typeInput : 'text'" :rules="rules"
        :name="name" :error="error" :value="dataValue" :style="{ 'text-align': textAlign }" :tabindex="tabIndex"
        @input="onInput" :maxlength="maxLength > 0 && maxLength" @blur="onBlur(name)" spellcheck="false" />
      <div class="input-label-wrapper" :class="!dataValue ? 'no-text' : 'has-text'" v-if="label"
        @click="handleClickLabel">
        <span class="input-label">{{ label }}</span>
      </div>
      <component :is="showIcon2 ? icon2 : icon1" class="input__icon" @click="clickIcon"></component>
      <div class="change_input_wrapper" v-if="isAdjustable">
        <div class="decrease" @click="handleDecrease">
          <micon type="Decrease" />
        </div>
        <div class="increase" @click="handleIncrease">
          <micon type="Increase" />
        </div>
      </div>

    </div>
    <div class="flex-between" v-if="checkError">
      <div v-show="error" class="input-error">{{ error }}</div>
      <div class="input_maxlength" v-if="typeComponent === 'textarea' && maxLength > 0">{{ computedModelValueLength
        }}/{{ maxLength }} ký tự
        được sử dụng
      </div>
    </div>
  </div>
</template>

<script>
import { validate } from "@/utils/validate.js";
export default {
  name: "InputPri",
  props: {
    label: {
      type: String,
      default: "",
    },
    typeInput: {
      type: String,
    },
    allowNumber: {
      type: Boolean,
      default: false,
    },
    allowDecimal: {
      type: Boolean,
      default: false,
    },
    modelValue: {
      type: String,
    },
    rules: {
      type: String,
    },
    textAlign: {
      type: String,
    },
    placeholderInput: {
      type: String,
      default: "",
    },
    tabIndex: {
      type: String,
    },
    formName: {
      type: String,
      default: "",
    },
    name: {
      type: String,
      default: "",
    },
    type: {
      type: String,
      default: "",
    },
    typeComponent: {
      type: String,
      default: "input",
    },
    icon1: {
      type: Object,
      required: true,
    },
    icon2: {
      type: Object,
      required: true,
    },
    clickIcon: {
      type: Function,
      required: false,
    },
    maxLength: {
      type: Number,
      default: 0,
    },
    checkError: {
      type: Boolean,
      default: true,
    },
    isAdjustable: {
      type: Boolean,
      default: false
    },
    defaultChangeValue: {
      type: Number,
      default: 0
    }
  },

  data() {
    return {
      dataValue: "",
      error: "",
      showIcon2: true,
      currentValue: 0,
    };
  },
  created() {
    this.assignDataValue(this.modelValue);
  },
  watch: {
    modelValue(newValue) {
      this.assignDataValue(newValue);
    },
  },
  computed: {
    computedModelValueLength() {
      return this.modelValue ? this.modelValue.length : 0;
    }
  },
  methods: {
    handleClickLabel() {
      this.$refs.minput.focus();
    },
    handleDecrease() {
      let result = 0;
      if ((this.currentValue - this.defaultChangeValue) > 0) {
        result = +this.currentValue - this.defaultChangeValue;
      }
      this.assignDataValue(result);
      this.$emit("update:modelValue", result);
    },
    handleIncrease() {
      let value = +this.currentValue + this.defaultChangeValue;
      this.assignDataValue(value);
      this.$emit("update:modelValue", value);
    },
    /**
     * Format lại giá trị hiển thị trong input theo định dạng.
     * @param {*} value Giá trị của input
     */
    assignDataValue(value) {
      if (this.allowNumber || this.allowDecimal) {
        if (value) {
          this.currentValue = Number(value);
          let formattedString = Number(value).toLocaleString("vi-VN");
          this.dataValue = formattedString;
        } else {
          return "";
        }
      } else {
        this.dataValue = value;
      }
    },

    /**
     * @description Handles the input event when the user types into the input field
     * @author nttue
     * Modified at (10/07/2023)
     */
    onInput(event) {
      this.$emit("onInput", event.target.value);
      if (this.allowNumber) {
        const value = +event.target.value.split(".").join("");
        this.$emit("update:modelValue", value);
      } else if (this.allowDecimal) {
        const value = event.target.value.split(".").join("");
        const newValue = value.split(",");
        let number = 0;
        if (!newValue[1]) {
          number = +newValue[0];
        } else {
          number = parseFloat(newValue[0] + "." + newValue[1]);
        }
        this.$emit("update:modelValue", number);
      } else {
        this.dataValue = event.target.value;
        this.$emit("update:modelValue", event.target.value);
      }

      if (this.rules) {
        this.validateData();
      }
    },
    /**
     * Function to validate the data based on the specified rules.
     * @author nttue 03.03.2024
     */
    validateData() {
      for (const rule of this.rules.split("|")) {
        const err = validate[rule](
          this.dataValue,
          this.$MResources[this.formName][this.name][rule]
        );
        if (err) {
          this.error = err;
          return;
        }
      }
      this.error = "";
    },
    /**
     * Select and focus the input when it is focused.
     * @author nttue 03.03.2024
     */
    focus() {
      this.$nextTick(() => {
        this.$refs.minput.focus();
      });
    },
    /**
     * @description hàm thiết lập chiều cao của input theo chiều cao của nội dung trong input
     * @author nttue 03.03.2024
     */
    setHeightInput(height) {
      this.$nextTick(() => {
        this.$refs.minput.style.height = height + 'px';
        this.$refs.minput.style.minHeight = height + 'px';
      });
    },

    /**
     * @description hàm xử lý sự kiện khi người dùng blur khỏi input
     * @param {name} name tên của input
     */
    onBlur(name) {
      if (this.type && this.dataValue) {
        this.dataValue = this.$ms.common.upperCaseName(this.dataValue);
      }
      if (
        this.rules?.includes("required") ||
        (this.rules && this.dataValue.trim())
      ) {
        this.validateData();
      }
    },
    /**
     * @description Set the error message for the input.
     * @param {String} error The error message.
     */
    setError(error) {
      this.error = error;

    }
  },
};
</script>

<style lang="scss">
@import "./input.scss";
</style>
