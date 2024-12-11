<template>
  <div class="search-stock">
    <div class="home__right_search">
      <msearch v-model="search" :placeholder="'Tìm kiếm mã'"></msearch>
    </div>
    <div class="home__right__bottom">
      <div class="home__right_title">
        <span>Mã</span>
      </div>
      <div class="home__right_table">
        <ctable :columns="columnConfigs.columns" :datax="columnConfigs.datax" />
      </div>
    </div>

  </div>


</template>

<script>
import * as config from './columnConfigs';
import StockAPI from '@/apis/StockAPI';
export default {
  name: "SearchStock",
  props: {},
  data() {
    return {
      columnConfigs: {
        columns: config.columns,
        datax: [],
      },
      search: '',
      orginalData: [],
    }
  },
  watch: {
    search(newValue) {
      if (newValue) {
        this.columnConfigs.datax = [...this.orginalData.filter(stock => stock.stock_code.toLowerCase().includes(newValue.toLowerCase())
        )];
      } else {
        this.columnConfigs.datax = [...this.orginalData];
      }
    },
  },
  async mounted() {
    let datas = await StockAPI.get();
    let dataConvert = datas.map(stock => {
      return {
        ...stock,
        different: Math.floor(Math.random() * 3),
        change_price: Math.floor(Math.random() * 10),
      }
    });
    var fakeData = this.generateFakeData(dataConvert);
    console.log(JSON.stringify(fakeData));
    this.columnConfigs.datax = [...dataConvert];
    this.orginalData = [...dataConvert];
  },
  methods: {
    
  },
}; </script>


<style lang="scss">
@import "./search-stock.scss";
</style>
