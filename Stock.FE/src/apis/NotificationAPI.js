import request from "@/utils/request.js";
import BaseAPI from "./BaseAPI";

class NotificationAPI extends BaseAPI {
  constructor() {
    super("Notifications");
  }

  updateUnRead(payload) {
    return request.put(this.url + "/update_unread?id=" + payload);
  }

 
}

const instance = new NotificationAPI();

export default instance;
