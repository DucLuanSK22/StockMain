<template>
  <div class="testimony">
    <div class="rating__stars">
      <micon v-for="i in numStars" :key="i" type="Stars" />
    </div>
    <div class="rating__review">
      <div class="rating__review-content">{{ review }}</div>
    </div>
    <div class="testimony-person">
      <div class="person-avatar">
        <img :src="customInfo.urlAvatar" alt="Ảnh đại diện" />
      </div>
      <div class="person-info">
        <div class="person-name">{{ customInfo.name }}</div>
        <div class="person-address">{{ customInfo.address }}</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MRating",
  props: {
    numStars: {
      type: Number,
      default: 5,
    },
    review: {
      type: String,
      default:
        "“Tôi thích sự tiện lợi của RepairPal và biết được chi phí trước khi sửa chữa. Ngoài ra, tôi thích các tiêu chuẩn và kỳ vọng do RepairPal đưa ra; nó loại bỏ tất cả các câu hỏi ra khỏi quá trình.”",
    },
    customInfo: {
      type: Object,
      default: () => ({
        name: "Nam Pham Quang",
        address: "Hanoi, Vietnam",
        urlAvatar:
          "https://cdn.chanhtuoi.com/uploads/2022/01/hinh-avatar-nam-deo-kinh.jpg",
      }),
    },
  },
  data() {},
  watch: {},

  methods: {},
};
</script>

<style lang="scss">
@import "./rating.scss";
</style>
