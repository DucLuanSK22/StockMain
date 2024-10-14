<template>
  <div class="input-default date-wrapper" :class="error ? 'error' : ''">
    <mdate-picker
      :error="error"
      class="date-picker"
      text-input
      inline
      auto-apply
      v-model="date"
      calendar-cell-class-name="dp-custom-cell"
      :day-names="['T2', 'T3', 'T4', 'T5', 'T6', 'T7', 'CN']"
    >
      <template
        #month-year="{ month, year, updateMonthYear, handleMonthYearChange }"
      >
        <div class="custom-month-year-component">
          <div class="datepicker-control" v-if="!isOpenSelectMonthYear">
            <div
              class="pointer icon"
              @click="
                () => {
                  if (handlePrevious($event, month)) {
                    handleMonthYearChange(false);
                  }
                }
              "
            >
              <micon type="Previous" />
            </div>
            <span class="month-year-label"
              >Tháng
              {{ month + 1 }}
              Năm
              {{ year }}</span
            >
            <div class="pointer icon" @click="handleMonthYearChange(true)">
              <micon type="Next" />
            </div>
          </div>
        </div>
      </template>
    </mdate-picker>
  </div>
</template>

<script>
export default {
  name: "MDatePicker",

  props: {
    // model value
    modelValue: {
      type: Date,
      default: new Date(),
    },
  },
  data() {
    return {
      error: "",
      date: this.modelValue,
      isOpenSelectMonthYear: false,
      isPickMonth: false,
      // Tháng được chọn.
      mouthSelected: "",
      // Năm được chọn.
      yearSelected: "",
    };
  },
  watch: {
    /**
     * Hàm update giá trị của date.
     */
    modelValue() {
      this.date = this.modelValue;
    },
    date(newValue) {
      this.$emit("update:modelValue", newValue);
    },
  },
  methods: {
    /**
     * Hàm reset lại năm.
     * @param {*} date
     */
    resetMonthYear(date) {
      try {
        let tempDate;

        if (!date) {
          tempDate = new Date(Date.now());
        } else {
          tempDate = new Date(date);
        }

        let tempYear = tempDate.getFullYear();
        this.years = [];

        for (let i = 0; i < 12; i++) {
          this.years.push({
            value: tempYear - i,
            label: tempYear - i,
          });
        }

        this.yearSelected = tempYear;
        this.monthSelected = tempDate.getMonth() + 1;
      } catch (e) {
        console.log(e);
      }
    },

    handlePrevious(e, month) {
      const curerntMonth = new Date().getMonth();

      if (curerntMonth == month) {
        return false;
      }

      return true;
    },
  },
};
</script>

<style lang="scss">
@import "./datepicker.scss";
</style>
