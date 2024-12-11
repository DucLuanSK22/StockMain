import request from "@/utils/request.js";
import BaseAPI from "./BaseAPI";

class StockAPI extends BaseAPI {
  constructor() {
    super("stock");
  } 
}

const instance = new StockAPI();

export default instance;
