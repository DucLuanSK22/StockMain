<template>
  <div class="table_container">
    <table class="table">
      <thead>
        <tr>
          <th v-for="(column, index) in columnConfigs" :key="index" :style="{
            'text-align': column.textAlign,
            width: column.width ? column.width + 'px' : null,
            minWidth: column.width ? column.width + 'px' : null,
          }" :ref="column.field" @click="handleSort(column)">
            <span class="th__content">
              <span>{{ column.headerName }}</span>
              <micon class="th_arrow" v-if="column.sort == 'asc'" type="ArrowUp" />
              <micon class="th_arrow" v-else-if="column.sort == 'desc'" type="ArrowDown" />
              <micon class="th_arrow" v-else type="DoubleArrow" />
            </span>
          </th>
        </tr>
      </thead>
      <tbody class="tbody">
        <tr v-for="(item, index) in dataResource" :key="index">
          <td v-for="(column, index) in columns" :key="index" :style="{
            'text-align': column.textAlign,
            width: column.width ? column.width + 'px' : null,
            minWidth: column.width ? column.width + 'px' : null,
          }" :ref="column.field" @click="handleClickRow(item)">
            <div class="row__content"
              :class="`${column.functionCustomColorTD ? 'td__content_wrapper ' + column.functionCustomColorTD(item[column.field]) : ''}`">
              <span
                :class="`td__content ${column.funcCusColor ? column.funcCusColor(item[column.field], item[column.keyColor]) : ''}`">
                {{
                  formatData(column,
                    item[column.field], item.difference)
                }}</span>
              <span v-if="column.groupRow" class="group-row">{{ item[column.groupRow] }}</span>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
    <div class="no-data" v-if="hasNoData">
      <img src="@/assets/img/no-data.png" alt="">
      <span class="no-data-text">{{ noDataText }}</span>

    </div>

  </div>
</template>

<script>
import Enums from '@/resources/enumeration';
export default {
  name: "CTable",
  props: {
    columns: {
      type: Array,
      required: true,
    },
    datax: {
      type: Array,
      required: true,
    },
    noDataText: {
      type: String,
      required: false,
      default: 'Không có dữ liệu',
    }
  },
  data() {
    return {
      dataResource: this.datax,
      columnConfigs: this.columns,
    }
  },
  watch: {
    datax() {
      this.dataResource = this.datax
    },
    columns() {
      this.columnConfigs = this.columns.map(item => {
        return {
          ...item,
          sort: ''
        }
      })
    }
  },
  computed: {
    hasNoData() {
      return this.datax.length == 0;
    }
  },

  methods: {
    handleSort(column) {
      const me = this;
      if (column.sort == 'asc') {
        column.sort = 'desc';
      } else if (column.sort == 'desc') {
        column.sort = '';
      } else {
        column.sort = 'asc';
      }
      this.columnConfigs.forEach(item => {
        if (item.field != column.field) {
          item.sort = '';
        }
      })
      if (column.type === Enums.EnumColumnType.Text) {
        if (column.sort == 'asc') {
          me.dataResource.sort((a, b) => a[column.field].localeCompare(b[column.field]));
        } else if (column.sort == 'desc') {
          me.dataResource.sort((a, b) => b[column.field].localeCompare(a[column.field]));
        }
      } else {
        if (column.sort == 'asc') {
          me.dataResource.sort((a, b) => a[column.field] - b[column.field]);
        } else if (column.sort == 'desc') {
          me.dataResource.sort((a, b) => b[column.field] - a[column.field]);
        }
      }
    },
    handleClickRow(row) {
      this.$emit('clickRow', row)
    },
    formatData(column, data, difference = null) {
      if (data == null) {
        return '-';
      }
      const { type } = column;
      switch (type) {
        case Enums.EnumColumnType.Number: {
          return data.toLocaleString('en-US');
        }
        case Enums.EnumColumnType.Decimal: {
          return data.toFixed(2).toLocaleString('en-US');
        }
        case Enums.EnumColumnType.Percent: {
          return `${data.toFixed(2)} %`;
        }
        case Enums.EnumColumnType.Time: {
          return this.formatDate(new Date(data));
        }
        case Enums.EnumColumnType.Enum: {
          for (let key in Enums[column.enumName]) {
            if (Enums[column.enumName][key] == data) {
              return this.$t(`i18nEnum.${column.enumName + '_' + key}`);
            }
          }
        }
        default: {
          return data;
        }
      }
    },
    formatDate(date) {
      const hours = date.getHours().toString().padStart(2, '0');
      const minutes = date.getMinutes().toString().padStart(2, '0');
      const day = date.getDate().toString().padStart(2, '0');
      const month = (date.getMonth() + 1).toString().padStart(2, '0');
      const year = date.getFullYear().toString();

      return `${hours}:${minutes} ${day}/${month}/${year}`;
    },
  },
};
</script>

<style lang="scss" scoped>
@import "./ctable.scss";
</style>
