import request from "@/utils/request.js";
import BaseAPI from "./BaseAPI";

class GarageAPI extends BaseAPI {
  constructor() {
    super("Booking"); 
  }

  /**
   * Get the make by garage ID.
   *
   * @param {type} garageId - description of parameter
   * @return {type} description of return value
   * @author nttue 17.03.2024
   */
  getMakeByGarageId(garageId) {
    return request.get(
      this.url + `/get_make_by_garage_id?garageId=${garageId}`
    );
  }

  /**
   * Get the make by garage ID.
   *
   * @param {type} garageId - description of parameter
   * @return {type} description of return value
   * @author nttue 17.03.2024
   */
  getGarageServices(carId) {
    return request.get(this.url + `/get_garage_services?carId=${carId}`);
  }
  /**
   * Retrieves models by garage ID, make, and year of manufacture.
   *
   * @param {string} garageId - The ID of the garage
   * @param {string} make - The make of the car
   * @param {string} model - The model of the car
   * @return {Promise} A Promise that resolves to the requested models
   * @author nttue 17.03.2024
   */
  getModelsByGarageId(garageId, make, model) {
    return request.get(
      `${this.url}/get_model_by_garage_id?garageId=${garageId}&make=${make}&model=${model}`
    );
  }

  /**
   * Get years by garage ID and make.
   *
   * @param {type} garageId - description of parameter
   * @param {type} make - description of parameter
   * @return {type} description of return value
   * @author nttue 17.03.2024
   */
  getYearsByGarageIdAndMake(garageId, make, model, year) {
    return request.get(
      `${this.url}/get_year_by_garage_id?garageId=${garageId}&make=${make}&model=${model}&year=${year}`
    );
  }

  /**
   * getEstimateService - A function to get an estimate service.
   *
   * @param {Object} payload - The payload for the request
   * @return {Promise} The promise of the request
   * @author nttue 17.03.2024
   */
  getEstimateService(payload) {
    return request.post(`${this.url}/get_estimate_service`, payload);
  }

  getGarageByEstimate(payload) {
    return request.post(`${this.url}/get_garage_by_estimate`, payload);
  }
}

const instance = new GarageAPI();

export default instance;
