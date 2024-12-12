<template>
  <button v-if="type == 'icon'" class="btn icon" :class="[borderType]">
    <component :is="icon" class="btn-icon"></component>
    {{ buttonText }}
  </button>
  <component :is="type === 'link' ? 'a' : 'button'" :href="href" v-else class="btn"
    :class="[disabled ? 'disabled' : '', borderType]">
    {{ buttonText }}
  </component>
</template>


<script>
export default {
  name: "MButton",
  props: {
    buttonText: {
      type: String,
      default: "",
    },
    type: {
      validator(value) {
        return ["default", "icon", "link"].includes(value);
      },
      type: String,
      default: "default",
    },
    icon: {
      type: Object,
      required: true,
    },
    disabled: {
      type: Boolean,
      default: false
    },
    href: {
      type: String,
      default: "",
    },
    // Sửa định nghĩa borderType
    borderType: {
      type: String,
      default: 'primary'
    }
  }
};

</script>

<style lang="scss" scoped>
@import "./button.scss";
</style>
