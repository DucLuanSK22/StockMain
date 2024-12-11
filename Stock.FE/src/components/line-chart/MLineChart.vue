<template>
  <div class="line_chart_wrapper">
    <div class="choose_period">
      <div class="choose_period_item" :class="{ 'active': item.key == currentPeriod }" v-for="(item) in periods"
        :key="item.key" @click="currentPeriod = item.key">{{ item.label }}</div>

    </div>
    <div class="line_chart">

      <Line :data="chartConfig.data[currentStock][currentPeriod]" :options="chartConfig.options" ref="chart1" />
    </div>
    <div class="line_chart__bottom">
      <div class="line_chart__bottom_item" @click="handleChooseStock(stock.stock_code)" v-for="stock in stocks"
        :key="stock.stock_code" :class="{ 'active': stock.stock_code == currentStock }">
        <div class="top">{{ stock.stock_code }}</div>
        <div class="middle" :class="`${computedColor(stock.different)}`">
          <div>{{ stock.total_volume }}</div>
          <div>{{ stock.change_price }}{{ stock.change_price_by_percent }}</div>
        </div>
        <div class="bottom">{{ stock.total_assets }}</div>
      </div>

    </div>

  </div>
</template>

<script>
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
} from 'chart.js';
import { Line } from 'vue-chartjs';
import * as chartConfig from './chartConfig.js';

ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
)

export default {
  name: 'App',
  components: {
    Line
  },
  mounted() {

  },
  data() {
    return {
      chartConfig,
      currentPeriod: '1W',
      periods: [
        {
          key: '1W',
          label: '1W',
        },
        {
          key: '1M',
          label: '1M',
        },
        {
          key: '6M',
          label: '6M',
        },
        {
          key: '1Y',
          label: '1Y',
        },
      ],
      currentStock: 'VNINDEX',
      stocks: [
        {
          stock_code: 'VNINDEX',
          total_volume: '1,273.96',
          total_assets: "15,481.693 tỷ",
          change_price: "6.34",
          change_price_by_percent: "(0.48%)",
          different: 0
        },
        {
          stock_code: 'VN30',
          total_volume: '1,273.96',
          total_assets: "15,481.693 tỷ",
          change_price: "6.34",
          change_price_by_percent: "(0.48%)",
          different: 1
        },
        {
          stock_code: 'VN30F1M',
          total_volume: '1,273.96',
          total_assets: "15,481.693 tỷ",
          change_price: "6.34",
          change_price_by_percent: "(0.48%)",
          different: 2
        }
      ]
    }
  },
  methods: {
    handleChooseStock(stockCode) {
      this.currentStock = stockCode;
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
