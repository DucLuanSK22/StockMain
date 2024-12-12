<template>
  <div class="asset">
    <div class="asset__left">
      <SearchStock />
    </div>
    <div class="asset__right">
      <div class="asset__right__title">Tổng quan tài sản</div>
      <div class="asset__right__content">
        <div class="doughnut mb-2">
          <div class="flex-between mb-2 gap-2">
            <div class="chart_title flex-between"><span>Tài sản ròng</span>
              <span>{{ ((asset[0] + asset[1]) / 1000000).toFixed(2) }} triệu</span>
            </div>
            <div>
              <mbutton @click="depositAmount" buttonText="Nạp tiền tài khoản" borderType="secondary" />
            </div>
          </div>
          <Doughnut :assetProps="asset" />
        </div>
        <div class="column-chart">
          <div class="chart_title flex-between"><span>Lịch sử tài sản</span>
            <div class="choose_period">
              <div class="choose_period_item" :class="{ 'active': item.key == currentPeriod }" v-for="(item) in periods"
                :key="item.key" @click="currentPeriod = item.key">{{ item.label }}</div>

            </div>
          </div>
          <ColumnChart :dataProps="historys" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Doughnut from '@/components/doughnut/Doughnut.vue';
import ColumnChart from '@/components/column-chart/ColumnChart.vue';
import SearchStock from '@/components/search-stock/SearchStock.vue';
import StockAPI from '@/apis/StockAPI';
export default {
  components: {
    Doughnut,
    SearchStock,
    ColumnChart
  },

  data() {
    return {
      currentPeriod: 0,
      periods: [
        {
          key: 0,
          label: '1W',
        },
        {
          key: 1,
          label: '1M',
        },
        {
          key: 2,
          label: '6M',
        },
        {
          key: 3,
          label: '1Y',
        },
      ],
      asset: [],
      historys: {},
    };
  },
  watch: {
    currentPeriod(newValue) {
      this.getAssetHistoryByUser(newValue);
    }
  },

  mounted() {
    this.getAssets();
    this.getAssetHistoryByUser();
  },

  methods: {

    /**
     * Hàm cập nhật giá tiền mặt của người dùng khi người dùng nạp tiền vào tài khoản
     * @returns {Promise<void>}
     */
    async depositAmount() {
      try {
        let user = this.$ms.cache.getCache("user");
        this.$store.commit("showLoading");
        await StockAPI.addCashByUser(user.user_id);
        this.$store.commit("hideLoading");
        this.$store.commit("showToast", {
          label: "Bạn đã nạp thành công 100 triệu vào tài khoản",
          type: 'success'
        });

        user.cash_value += 100000000;
        user.total_net_assets += 100000000;

        this.$ms.cache.setCache("user", user);

        await this.getAssets();
        await this.getAssetHistoryByUser();
      } catch (error) {
        console.log(error);
        this.$store.commit("hideLoading");
        this.$store.commit("showToast", {
          label: "Nạp tiền không thành công. Vui lòng nạp lại.",
          type: 'error'
        });
      }
    },
    /**
     * Hàm getAssets:
     * - Lấy user hiện tại đang login trong cache.
     * - Nếu user có tồn tại (nghĩa là user đã login) thì lấy giá trị tài sản và tiền mặt của user.
     * - Gán giá trị tài sản và tiền mặt vào mảng asset.
     */
    getAssets() {
      // Lấy user hiện tại đang login trong cache
      let user = this.$ms.cache.getCache("user");

      // Nếu user có tồn tại (nghĩa là user đã login) thì lấy giá trị tài sản và tiền mặt của user
      if (user) {
        // Gán giá trị tài sản và tiền mặt vào mảng asset
        this.asset = [user.stock_value, user.cash_value];
      }
    },
    /**
     * Hàm getAssetHistoryByUser:
     * - Lấy user hiện tại đang login trong cache.
     * - Nếu user có tồn tại (nghĩa là user đã login) thì lấy lịch sử tài sản và tiền mặt của user.
     * - Gán giá trị lịch sử tài sản và tiền mặt vào mảng historys.
     * @param currentPeriod 
     */
    async getAssetHistoryByUser(currentPeriod = 0) {
      const me = this;
      try {
        // Lấy user hiện tại đang login trong cache
        let user = this.$ms.cache.getCache("user");
        if (user) {
          me.$store.commit("showLoading");
          const result = await StockAPI.getAssetHistoryByUser(
            {
              user_id: user.user_id,
              periodEnum: currentPeriod
            }
          );
          me.$store.commit("hideLoading");
          let cashValues = result.map(item => item.cash_value);
          let stockValues = result.map(item => item.stock_value);
          this.historys = {
            data: [
              {
                name: 'Tiền',
                data: [...cashValues]
              },
              {
                name: 'Cổ phiếu ',
                data: [...stockValues]

              }
            ],
            originData: result
          }
          if (result.length > 0) {
            let asset = result[result.length - 1];
            this.asset = [asset.stock_value, asset.cash_value];
          }
        }
      } catch (error) {
        console.log(error);
        me.$store.commit("hideLoading");
      }
    }
  },
};
</script>

<style lang="scss" scoped>
@import "./asset.scss";

.choose_period {
  display: flex;
  align-items: center;
  justify-content: flex-start;
  gap: 12px;

  .choose_period_item {
    box-shadow: rgba(0, 0, 0, 0.1) 0px 2px 2px;
    padding: 6px 12px;
    line-height: 1.125;
    height: 28px;
    background-color: rgba(51, 51, 51, 0.5);
    font-weight: 500;
    border-radius: 4px;
    cursor: pointer;

    &.active {
      background-color: rgb(212, 67, 67);
      color: rgb(228, 228, 236);
    }
  }
}
</style>
