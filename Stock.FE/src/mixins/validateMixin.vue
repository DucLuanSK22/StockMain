<template>
  <div></div>
  <mloading v-if="showLoading"></mloading>
</template>
<script>
import insertError from "@/utils/insertError.js";
export default {
  name: "validateFormMixin",
  data() {
    return {
      showLoading: false,
    };
  },
  computed: {
    /**
     * @return trả vể 1 mảng chứa tên của các ref trong form.
     * @author nttue (07/07/2023)
     */
    refsList() {
      return Object.keys(this.$refs);
    },
  },
  methods: {
    /**
     * Handle validation of input fields.
     *
     * @return {boolean} true if there is an error, false otherwise
     * @author nttue 03.03.2024
     */
    handleValidate() {
      try {
        let firstErrorIndex = 9999;
        let hasError = false;
        for (let i = 0; i < this.refsList.length; i++) {
          let inputRef = this.$refs[this.refsList[i]];

          if (
            (inputRef?.rules && inputRef.rules.includes("required")) ||
            inputRef?.error
          ) {
            const err = insertError(inputRef, this.formName);
            if (err) {
              hasError = true;
              firstErrorIndex = Math.min(firstErrorIndex, i);
              inputRef.error = err;
            } else if (inputRef.error && !err) {
              hasError = true;
              firstErrorIndex = Math.min(firstErrorIndex, i);
            } else {
              inputRef.error = "";
            }
          }
        }
        return hasError;
      } catch (error) {
        console.error(error);
      }
    },
    /**
     * Toggles the visibility of the password input field.
     * @author nttue 03.03.2024
     */
    handleSwitchVisibility() {
      const passwordRef = this.$refs.Password;
      if (passwordRef.$refs.minput.type === "password") {
        passwordRef.$refs.minput.type = "text";
        passwordRef.showIcon2 = false;
      } else {
        passwordRef.showIcon2 = true;
        passwordRef.$refs.minput.type = "password";
      }
    },
  },
};
</script>
