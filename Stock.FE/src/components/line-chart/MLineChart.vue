<template>
  <div class="line_chart_wrapper">
    <div class="choose_period">
      <div class="choose_period_item" :class="{ 'active': item.key == currentPeriod }" v-for="(item) in periods"
        :key="item.key" @click="currentPeriod = item.key">{{ item.label }}</div>

    </div>
    <div class="line_chart">
      <apexchart type="area" height="320" ref="chart" :options="chartOptions" :series="series">
      </apexchart>
    </div>
    <div class="line_chart__bottom">
      <div class="line_chart__bottom_item" @click="handleChooseStock(stock.stock_id)" v-for="stock in stocks"
        :key="stock.stock_id" :class="{ 'active': stock.stock_id == currentStock }">
        <div class="top">{{ stock.stock_code }}</div>
        <div class="middle" :class="`${computedColor(stock.different)}`">
          <div>{{ stock.total_volume }}</div>
          <div>{{ stock.change_price }}({{ stock.change_price_by_percent }}%)</div>
        </div>
        <div class="bottom">{{ stock.total_assets }}</div>
      </div>

    </div>

  </div>
</template>

<script>
import { configOptions } from './chartConfig.js';
import StockAPI from '@/apis/StockAPI.js';

export default {
  name: 'MLineChart',

  async mounted() {
    await this.getPopularStock();
  },
  data() {
    return {
      ...configOptions,
      currentPeriod: 1,
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
      currentStock: '',
      stocks: [
      ],
    }
  },
  watch: {
    async currentPeriod() {
      await this.getData();
    },

    async currentStock() {
      await this.getData();
    }
  },
  methods: {

    async getPopularStock() {
      let result = await StockAPI.getPopularStock();
      this.stocks = result.map(item => {
        return {
          ...item,
          change_price_by_percent: (Math.random()).toFixed(2),
          change_price: (Math.random() * 10).toFixed(2),
          different: Math.floor(Math.random() * 3),
          total_assets: this.formatToBillion(item.total_assets),
          total_volume: this.formatAmount(item.total_volume),
        }
      });
      this.currentStock = this.stocks[0].stock_id;

    },

    formatToBillion(num) {
      let billion = (num / 1_000_000_000).toFixed(2);
      return `${billion} tỉ`;
    },
    formatAmount(amount) {
      if (amount) {
        let formattedAmount = amount
          .toString()
          .replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        return formattedAmount;
      } else {
        return "";
      }
    },
    async getData() {
      let result = await StockAPI.getStockByPeriod({ stockId: this.currentStock, periodEnum: this.currentPeriod });
      if (result.length > 0) {

        this.series[0].data = result.map(item =>
          [new Date(item.modified_at), item.current_price.toFixed(2)],
        );
      }
    },

    handleChooseStock(stockId) {
      this.currentStock = stockId;
    },
    computedColor(different) {
      switch (different) {
        case 0:
          return 'green';
        case 1:
          return 'red';
        case 2:
          return 'yellow';
        default:
          break;
      }

    }
  }
}
</script>
<style lang="scss" scoped>
@import url('./line-chart.scss');
</style>
