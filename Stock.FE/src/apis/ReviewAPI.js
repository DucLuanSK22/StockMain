import request from "@/utils/request.js";
import BaseAPI from "./BaseAPI";

class ReviewAPI extends BaseAPI {
  constructor() {
    super("Reviews"); 
  }

}

const instance = new ReviewAPI();

export default instance;
