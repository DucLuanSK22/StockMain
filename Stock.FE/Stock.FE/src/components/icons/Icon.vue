<template>
  <Close v-if="type == 'Close'" />
  <Increase v-if="type == 'Increase'" />
  <Decrease v-if="type == 'Decrease'" />
  <Eye v-if="type == 'Eye'" />
  <HideEye v-if="type == 'HideEye'" />
  <ArrowLeft v-if="type == 'ArrowLeft'" />
  <Logo v-if="type == 'Logo'" />
  <ArrowRight v-if="type == 'ArrowRight'" />
  <Avatar v-if="type == 'Avatar'" />
  <Search v-if="type == 'Search'" />
  <Wallet v-if="type == 'Wallet'" />
  <ArrowDown v-if="type == 'ArrowDown'" />
  <ArrowUp v-if="type == 'ArrowUp'" />
  <DoubleArrow v-if="type == 'DoubleArrow'" />
</template>

<script>
import Close from "./Close.vue";
import Increase from "./Increase.vue";
import Decrease from "./Decrease.vue";
import Eye from "./Eye.vue";
import HideEye from "./HideEye.vue";
import ArrowLeft from "./ArrowLeft.vue";
import ArrowRight from "./ArrowRight.vue";
import Logo from "./Logo.vue";
import Avatar from "./Avatar.vue";
import Search from "./Search.vue";
import Wallet from "./Wallet.vue";
import ArrowDown from "./ArrowDown.vue";
import ArrowUp from "./ArrowUp.vue";
import DoubleArrow from "./DoubleArrow.vue";

export default {
  name: "Icon",
  props: {
    type: {
      type: String,
      default: "",
    },
    height: {
      type: Number,
      default: 13
    },
    width: {
      type: Number,
      default: 13
    }
  },
  components: {
    Close,
    Increase,
    Decrease,
    Eye,
    HideEye,
    ArrowLeft,
    Logo,
    ArrowRight,
    Avatar,
    Search,
    Wallet,
    ArrowDown,
    ArrowUp,
    DoubleArrow
  },
};
</script>
