<template>
  <div class="line_chart_wrapper" ref="MLineChart">
    <div class="choose_period" ref="choose_period">
      <div class="choose_period_item" :class="{ 'active': item.key == currentPeriod }" v-for="(item) in periods"
        :key="item.key" @click="currentPeriod = item.key">{{ item.label }}</div>

    </div>
    <div class="line_chart">
      <apexchart type="area" :height="chartHeight" ref="chart" :options="chartOptions" :series="series">
      </apexchart>
    </div>
    <div class="line_chart__bottom" ref="line_chart__bottom">
      <div class="line_chart__bottom_item" @click="handleChooseStock(stock.IndexId)" v-for="stock in stocks"
        :key="stock.IndexId" :class="{ 'active': stock.IndexId == currentStock }">
        <div class="top">
          <div class="dot" :class="`${computedColor(stock.Change)}`" v-if="stock.IndexId == currentStock"></div>
          <span>{{ stock.IndexName }}</span>
        </div>
        <div class="middle" :class="`${computedColor(stock.Change)}`">
          <div>{{ stock.IndexValue.replace(/\B(?=(\d{3})+(?!\d))/g, ',') }}</div>
          <div>{{ stock.Change }}({{ Number(stock.RatioChange).toFixed(2) }}%)</div>
        </div>
        <div class="bottom">{{ formatToBillion(stock.TotalVal).replace(/\B(?=(\d{3})+(?!\d))/g, ',') }}</div>
      </div>

    </div>

  </div>
</template>

<script>
import { configOptions } from './chartConfig.js';
import MarketAPI from '@/apis/MarketAPI.js';

export default {
  name: 'MLineChart',
  created() {
    this.calculateHeight();
  },
  mounted() {
    this.getPopularStock();
  },
  data() {
    return {
      ...configOptions,
      chartHeight: 300,
      currentPeriod: 1,
      periods: [
        {
          key: 0,
          label: '1W',
        },
        {
          key: 1,
          label: '1M',
        }
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
    calculateHeight() {
      const me = this;
      me.chartHeight = window.innerHeight - 240;
    },

    async getPopularStock() {
      const me = this;
      try {
        me.$store.commit("showLoading");
        let result = await MarketAPI.getPopularStock();
        me.$store.commit("hideLoading");
        me.stocks = result.map(item => {
          let stock = JSON.parse(item).data[0]
          return {
            ...stock,

          }
        });
        me.currentStock = me.stocks[0].IndexId;

      } catch (error) {
        console.log(error);
        me.$store.commit("hideLoading");
      }
    },

    formatToBillion(num) {
      let billion = (num / 1_000_000_000).toFixed(2);
      return `${billion} tỉ`;
    },
    async getData() {
      const context = this;
      try {
        context.$store.commit("showLoading");

        const response = await MarketAPI.getStockByPeriod({
          indexId: context.currentStock,
          periodEnum: context.currentPeriod
        });

        context.$store.commit("hideLoading");
        if (response.data.length > 0) {
          context.series[0].data = response.data.map(item => [
            new Date(item.TradingDate.split('/').reverse().join('-')),
            item.IndexValue
          ]);

          const lastChange = response.data[response.data.length - 1].Change;
          let color = '#ffad0d';

          if (lastChange > 0) {
            color = '#34c85e';
          } else if (lastChange < 0) {
            color = '#ff4242';
          }

          context.chartOptions = {
            ...context.chartOptions,
            colors: [color],
          };
        }
      } catch (error) {
        console.error(error);
        context.$store.commit("hideLoading");
      }
    },

    handleChooseStock(stockId) {
      this.currentStock = stockId;
    },
    computedColor(value) {
      if (value > 0) {
        return 'green';
      } else if (value < 0) {
        return 'red';
      } else {
        return 'yellow';
      }

    }
  }
}
</script>
<style lang="scss" scoped>
@import url('./line-chart.scss');
</style>
