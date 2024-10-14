import request from "@/utils/request.js";
import BaseAPI from "./BaseAPI";

class PromoAPI extends BaseAPI {
  constructor() {
    super("Promotion");
  }

  
  getPromoFilter(timeFilterType, carName) {
    return request.get(this.url + "/get_filter?" + `timeFilterType=${timeFilterType}&carName=${carName}`);
  }
  
  getPromoById(promoId) {
    return request.get(this.url + `/${promoId}`);
  }


 
}

const instance = new PromoAPI();

export default instance;
