<template>
  <Background v-if="type == 'Background'" />
  <Close v-if="type == 'Close'" />
  <Increase v-if="type == 'Increase'" />
  <Decrease v-if="type == 'Decrease'" />
  <Eye v-if="type == 'Eye'" />
  <HideEye v-if="type == 'HideEye'" />
  <Set v-if="type == 'Set'" />
  <ChevronLeft v-if="type == 'ChevronLeft'" />
  <ChevronRight v-if="type == 'ChevronRight'" />
  <ChevronDown v-if="type == 'ChevronDown'" />
  <ChevronTop v-if="type == 'ChevronTop'" />
  <ArrowLeft v-if="type == 'ArrowLeft'" />
  <Books v-if="type == 'Books'" />
  <Car v-if="type == 'Car'" />
  <Logo v-if="type == 'Logo'" />
  <Previous v-if="type == 'Previous'" />
  <Next v-if="type == 'Next'" />
  <ArrowRight v-if="type == 'ArrowRight'" />
  <Stars v-if="type == 'Stars'" :width="width" :height="height" />
  <StarsV2 v-if="type == 'StarsV2'" :width="width" :height="height" />
  <Note v-if="type == 'Note'" />
  <Avatar v-if="type == 'Avatar'" />
  <Search v-if="type == 'Search'" />
  <Location v-if="type == 'Location'" />
  <Info v-if="type == 'Info'" />
  <Tag v-if="type == 'Tag'" />
  <PricePlus v-if="type == 'PricePlus'" />
  <ShareUp v-if="type == 'ShareUp'" />
  <Repair v-if="type == 'Repair'" />
  <Price v-if="type == 'Price'" />
  <Certify v-if="type == 'Certify'" />
  <Yes v-if="type == 'Yes'" />
  <No v-if="type == 'No'" />
  <Question v-if="type == 'Question'" />
  <Certification v-if="type == 'Certification'" />
  <CarLarge v-if="type == 'CarLarge'" />
  <Calendar v-if="type == 'Calendar'" />
  <Notify v-if="type == 'Notify'" />
  <Check v-if="type == 'Check'" />
  <Popular v-if="type == 'Popular'" />
  <Answer v-if="type == 'Answer'" />
  <Wallet v-if="type == 'Wallet'" />
</template>

<script>
import Background from "./Background.vue";
import Close from "./Close.vue";
import Increase from "./Increase.vue";
import Decrease from "./Decrease.vue";
import Eye from "./Eye.vue";
import HideEye from "./HideEye.vue";
import ChevronLeft from "./ChevronLeft.vue";
import ChevronRight from "./ChevronRight.vue";
import ChevronDown from "./ChevronDown.vue";
import ChevronTop from "./ChevronTop.vue";
import ArrowLeft from "./ArrowLeft.vue";
import ArrowRight from "./ArrowRight.vue";
import Books from "./Books.vue";
import Car from "./Car.vue";
import Logo from "./Logo.vue";
import Previous from "./Previous.vue";
import Next from "./Next.vue";
import Stars from "./Stars.vue";
import Note from "./Note.vue";
import Avatar from "./Avatar.vue";
import Search from "./Search.vue";
import Location from "./Location.vue";
import Info from "./Info.vue";
import Tag from "./Tag.vue";
import PricePlus from "./PricePlus.vue";
import ShareUp from "./ShareUp.vue";
import Repair from "./Repair.vue";
import Price from "./Price.vue";
import Certify from "./Certify.vue";
import Yes from "./Yes.vue";
import No from "./No.vue";
import Question from "./Question.vue";
import Certification from "./Certification.vue";
import CarLarge from "./CarLarge.vue";
import Calendar from "./Calendar.vue";
import Notify from "./Notify.vue";
import Check from "./Check.vue";
import Popular from "./Popular.vue";
import Answer from "./Answer.vue";
import StarsV2 from "./StarsV2.vue";
import Wallet from "./Wallet.vue";

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
    Background,
    Close,
    Increase,
    Decrease,
    Eye,
    HideEye,
    Set,
    ChevronLeft,
    ChevronRight,
    ChevronDown,
    ArrowLeft,
    Books,
    Car,
    Logo,
    Previous,
    Next,
    ArrowRight,
    ChevronTop,
    Stars,
    Note,
    Avatar,
    Search,
    Location,
    Info,
    Tag,
    PricePlus,
    ShareUp,
    Repair,
    Price,
    Certify,
    Yes,
    No,
    Question,
    Certification,
    CarLarge,
    Calendar,
    Notify,
    Check,
    Popular,
    Answer,
    StarsV2,
    Wallet
  },
};
</script>
