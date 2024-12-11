import request from "@/utils/request.js";
import BaseAPI from "./BaseAPI";

class MarketAPI extends BaseAPI {
  constructor() {
    super("market");
  }
  getPopularStock() {
    return request.get(this.url + "/get_popular_stock");
  }
  getStockByPeriod(payload) {
    return request.get(this.url + `/get_stock_by_period?indexId=${payload.indexId}&periodEnum=${payload.periodEnum}`);
  }
}

const instance = new MarketAPI();

export default instance;
