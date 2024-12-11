<template>
  <div class="purchase">
    <div class="purchase__left">
      <div class="tab">
        <div class="tab_header">
          <div class="tab_item" :class="{ 'active': item.tabKey == currentTab }" v-for="item in listTabs"
            :key="item.tabKey" @click="currentTab = item.tabKey">
            {{ item.title }}
          </div>

        </div>
        <div class="tab_content">
          <ctable v-if="currentTab == 0" :columns="columnsConfigs.normalCommandColumns"
            :datax="columnsConfigs.normalCommandDatax" />
          <ctable v-else :columns="columnsConfigs.dealColumns" :datax="columnsConfigs.dealDatax"
            @clickRow="handleChooseDeal" />
        </div>
      </div>
    </div>
    <div class="purchase__right">
      <div class="tab">
        <div class="tab_header">

          <div class="tab_item active">Lệnh thường</div>

        </div>
        <div class="tab_content">
          <div class="stock_search">

            <msearch v-model="search" :placeholder="'Tìm kiếm mã'"></msearch>
          </div>
          <div class="stock">
            <div class="stock_top">
              <div class="stock_top_right">
                <div class="stock_code">
                  <span class="code">ACB</span>
                  <span class="flatform">HOSE</span>
                </div>
                <div class="stock_name">Ngân hàng Á Châu</div>
              </div>
              <div class="stock_top_left green">
                <span class="matching_price">24.80
                </span>
                <span class="price_changes">(0.15 </span>&nbsp;
                <span class="price_percent">0.00%)</span>


              </div>
            </div>
            <div class="stock_bottom">
              <div class="price_item">
                <div class="price_item_title">Sàn</div>
                <div class="price_item_value blue">24.80</div>
              </div>
              <div class="price_item">
                <div class="price_item_title">Tham chiếu</div>
                <div class="price_item_value yellow">24.80</div>
              </div>
              <div class="price_item">
                <div class="price_item_title">Trần</div>
                <div class="price_item_value purple">24.80</div>
              </div>
            </div>

          </div>
          <div class="purchasing_ability">
            <div class="purchasing_ability_title">Sức mua</div>
            <div class="purchasing_ability_value">34,326</div>
          </div>

          <div class="set_price">
            <div class="set_price_title">Giá đặt</div>
            <minput :isAdjustable="true" name="order_price" v-model="objectMaster.order_price" ref="order_price"
              rules="required|maxlength_100" :allowDecimal="true" :defaultChangeValue="0.15" :checkError="false"
              class="set_price_input border row-100" />

          </div>

          <div class="set_price">
            <div class="set_price_title">KL đặt</div>
            <minput name="volume" :isAdjustable="true" v-model="objectMaster.volume" ref="volume"
              rules="required|maxlength_100" :allowNumber="true" :defaultChangeValue="100" :checkError="false"
              class="set_price_input border row-100" />

          </div>
          <div class="purchase_action">
            <div class="buy">
              <div class="title">MUA</div>
              <div>{{ objectMaster.buying_ability }}</div>
            </div>
            <div class="sell">
              <div class="title">BÁN</div>
              <div>{{ objectMaster.purchasing_ability }}</div>
            </div>
          </div>
        </div>
      </div>

    </div>
  </div>
</template>

<script>
import * as config from './columnsConfig';
export default {

  data() {
    return {
      objectMaster: {
        buying_ability: 0,
        purchasing_ability: 0,
      },
      listTabs: [
        {
          tabKey: 0,
          title: 'Sổ lệnh thường',
        },
        {
          tabKey: 1,
          title: 'Deal nắm giữ',
        },
      ],
      currentTab: 0,
      columnsConfigs: config,
      asset: {
        cash: 11897011
      }
    };
  },

  async mounted() {

  },

  methods: {
    handleChooseDeal(deal) {
      const me = this;
      me.objectMaster.buying_ability = deal.total_tradeable_volume;
      me.objectMaster.purchasing_ability = Math.floor(me.asset.cash / deal.current_price).toLocaleString('en-US');

    }
  },
};
</script>

<style lang="scss">
@import "./purchase.scss";
</style>
