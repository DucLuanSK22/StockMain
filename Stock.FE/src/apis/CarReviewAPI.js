import request from "@/utils/request.js";
import BaseAPI from "./BaseAPI";

class CarReviewAPI extends BaseAPI {
  constructor() {
    super("CarReview");
  }


    /**
     * Handle like or unlike a post.
     * @param {Object} object - The object to be passed in the request
     * @return {Promise} The promise of the request
     */
    async likeOrUnLikeAsync(object) {
    return request.post(this.url + "/like_or_unlike", object);
  }

  getQuestionPopular(userId) {
    return request.get(
      this.url + `/get_questions_popular?userId=${userId}`
    );
  }

  /**
   * Retrieves a question using the car ID.
   *
   * @param {type} make - The make of the car to get the question for
   * @return {type} The result of the request
   */
  getQuestionByCar(make, userId) {
    return request.get(
      this.url + `/get_questions_by_car?make=${make}&userId=${userId}`
    );
  }

  getOverview(make = '') {
    if (make) {
      return request.get(
        this.url + `/get_overview_rating?make=${make}`
      );
    } else {
      return request.get(
        this.url + `/get_overview_rating`
      );
    }
  }
 
  searchQuestionByTitle(title) {
    return request.get(
      this.url + `/search_questions_by_title?title=${title}`
    );
  }
GetQuestionPopular() {
    return request.get(
      this.url + `/get_questions_popular`
    );
  }
GetQuestionByMake(make, userId) {
    return request.get(
      this.url + `/get_questions_by_make?make=${make}&userId=${userId}`
    );
  }

  GetQuestionByModel(make, model, userId) {
    return request.get(
     this.url + `/get_questions_by_model?make=${make}&model=${model}&userId=${userId}`
    );
  }
}

const instance = new CarReviewAPI();

export default instance;
