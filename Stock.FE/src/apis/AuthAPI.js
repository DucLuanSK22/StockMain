import request from "@/utils/request.js";
import BaseAPI from "./BaseAPI";

class AuthAPI extends BaseAPI {
  constructor() {
    super("Users");
  }

  login(payload) {
    return request.post(this.url + "/login", payload);
  }

  register(payload) {
    return request.post(this.url + "/register", payload);
  }
  /**
   * Retrieve a new password for the given email.
   *
   * @param {string} email - The email for which to retrieve a new password
   * @return {Promise} The request promise for getting the new password
   */
  getNewPassword(email) {
    return request.get(`${this.url}/get_new_password`, {
      params: {
        email,
      },
    });
  }
}

const instance = new AuthAPI();

export default instance;
