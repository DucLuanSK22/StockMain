<template>
  <div class="home">


    <div class="home__right">
      <SearchStock />
    </div>
    <div class="home__center">
      <div class="home__center_balance">
        <div class="right">
          <div class="right_currency">16,676,504</div>
          <div class="right_title">Hôm nay khoản đầu tư của Luân tăng</div>
          <div class="right_amount">+240,000</div>
        </div>
        <div class="left">
          <micon type="Wallet" />
        </div>
      </div>
      <MLineChart />

    </div>
    <div class="home__left">
      <div class="home__left_title">Tin vĩ mô</div>

      <div class="list_news">
        <div class="list_news_item" v-for="(item, index) in news" :key="index">
          <div class="left">
            <div class="title">
              {{ item.title }}
            </div>
            <div class="time">10h trước</div>
          </div>
          <div class="right">
            <img :src="item.image" />

          </div>
        </div>

      </div>
    </div>
  </div>
</template>

<script>
import SideBar from "@/components/sidebar/SideBar.vue";
import MLineChart from '@/components/line-chart/MLineChart.vue';
import SearchStock from '@/components/search-stock/SearchStock.vue';
export default {
  components: {
    SideBar,
    MLineChart,
    SearchStock
  },
  data() {
    return {
      news: [
        {
          title: 'Được gì từ loạt động thái ‘hạ nhiệt’ tỷ giá?',
          image: 'https://image.tienphong.vn/600x315/Uploaded/2024/unvjthp/2024_06_27/u-5921.jpg'
        },
        {
          title: 'Được gì từ loạt động thái ‘hạ nhiệt’ tỷ giá?',
          image: 'https://image.tienphong.vn/600x315/Uploaded/2024/unvjthp/2024_06_27/u-5921.jpg'
        },
        {
          title: 'Được gì từ loạt động thái ‘hạ nhiệt’ tỷ giá?',
          image: 'https://image.tienphong.vn/600x315/Uploaded/2024/unvjthp/2024_06_27/u-5921.jpg'
        },
        {
          title: 'Được gì từ loạt động thái ‘hạ nhiệt’ tỷ giá?',
          image: 'https://image.tienphong.vn/600x315/Uploaded/2024/unvjthp/2024_06_27/u-5921.jpg'
        },
        {
          title: 'Được gì từ loạt động thái ‘hạ nhiệt’ tỷ giá?',
          image: 'https://image.tienphong.vn/600x315/Uploaded/2024/unvjthp/2024_06_27/u-5921.jpg'
        },
        {
          title: 'Được gì từ loạt động thái ‘hạ nhiệt’ tỷ giá?',
          image: 'https://image.tienphong.vn/600x315/Uploaded/2024/unvjthp/2024_06_27/u-5921.jpg'
        },
        {
          title: 'Được gì từ loạt động thái ‘hạ nhiệt’ tỷ giá?',
          image: 'https://image.tienphong.vn/600x315/Uploaded/2024/unvjthp/2024_06_27/u-5921.jpg'
        },
        {
          title: 'Được gì từ loạt động thái ‘hạ nhiệt’ tỷ giá?',
          image: 'https://image.tienphong.vn/600x315/Uploaded/2024/unvjthp/2024_06_27/u-5921.jpg'
        },
        {
          title: 'Được gì từ loạt động thái ‘hạ nhiệt’ tỷ giá?',
          image: 'https://image.tienphong.vn/600x315/Uploaded/2024/unvjthp/2024_06_27/u-5921.jpg'
        },
        {
          title: 'Được gì từ loạt động thái ‘hạ nhiệt’ tỷ giá?',
          image: 'https://image.tienphong.vn/600x315/Uploaded/2024/unvjthp/2024_06_27/u-5921.jpg'
        },
        {
          title: 'Được gì từ loạt động thái ‘hạ nhiệt’ tỷ giá?',
          image: 'https://image.tienphong.vn/600x315/Uploaded/2024/unvjthp/2024_06_27/u-5921.jpg'
        },
        {
          title: 'Được gì từ loạt động thái ‘hạ nhiệt’ tỷ giá?',
          image: 'https://image.tienphong.vn/600x315/Uploaded/2024/unvjthp/2024_06_27/u-5921.jpg'
        },
      ],
    };
  },

  mounted() {
  },

  methods: {},
};
</script>

<style lang="scss">
@import "./home.scss";
</style>
