import request from "@/utils/request.js";
import BaseAPI from "./BaseAPI";

class StockAPI extends BaseAPI {
  constructor() {
    super("stock");
  }

  register(payload) {
    return request.post(this.url + "/register", payload);
  }

  login(payload) {
    return request.post(this.url + "/login", payload);
  }

  addCashByUser(userId) {
    return request.get(this.url + `/add_cash?userId=${userId}`);
  }

  getAssetHistoryByUser(payload) {
    return request.get(this.url + `/get_asset_history_by_user?userId=${payload.user_id}&periodEnum=${payload.periodEnum}`);
  }

  purchase(payload) {
    return request.post(this.url + "/purchase", payload);
  }


  getDealsAndTransactions(userId) {
    return request.get(this.url + `/get_transaction?userId=${userId}`);
  }
  getStockByIdAsync(stockId) {
    return request.get(this.url + `/get_stock_by_id?stockId=${stockId}`);
  }

  getAssetDashboard(userId) {
    return request.get(this.url + `/get_asset_dashboard?userId=${userId}`);
  }
  getReportStock() {
    return request.get(this.url + `/get_report_stock`);
  }
}

const instance = new StockAPI();

export default instance;
