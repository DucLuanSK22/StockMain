<template>
  <div class="home">


    <div class="home__right">
      <SearchStock />
    </div>
    <div class="home__center">
      <div class="home__center_balance">
        <div class="right">
          <div class="right_currency">{{ assets.total_net_assets }}</div>
          <div class="right_title">Hôm nay khoản đầu tư của Luân {{ assets.profit > 0 ? 'tăng' : 'giảm' }}</div>
          <div class="right_amount" :class="assets.profit > 0 ? 'green' : 'red'">{{ assets.profit > 0 ? '+' : '' }}{{
            assets.profit }}</div>
        </div>
        <div class="left pointer" @click="goToWallet">
          <micon type="Wallet" />
        </div>
      </div>
      <MLineChart />

    </div>
  </div>
</template>

<script>
import MLineChart from '@/components/line-chart/MLineChart.vue';
import SearchStock from '@/components/search-stock/SearchStock.vue';

import StockAPI from '@/apis/StockAPI';
export default {
  components: {
    MLineChart,
    SearchStock
  },
  data() {
    return {
      assets: {
        total_net_assets: 0,
        profit: 0
      }
    };
  },

  mounted() {
    this.getAssetDashboard();

  },

  methods: {
    goToWallet() {
      const me = this;
      me.$router.push({
        path: "/asset",
      });
    },
    async getAssetDashboard() {
      const me = this;
      try {

        let user = me.$ms.cache.getCache("user");
        me.user = user;
        if (user) {
          me.$store.commit("showLoading")
          var result = await StockAPI.getAssetDashboard(user.user_id);
          me.$store.commit("hideLoading")
          if (result.total_net_assets) {

            me.assets = {
              total_net_assets: result.total_net_assets.toLocaleString('en-US'),
              profit: result.profit.toLocaleString('en-US')

            };
          }
        }
      }
      catch (error) {
        console.log(error);
        me.$store.commit("hideLoading")
      }
    },
  }
}
</script>

<style lang="scss">
@import "./home.scss";
</style>
