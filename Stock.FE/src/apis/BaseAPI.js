import request from "@/utils/request.js";

class BaseAPI {
  /**
   * @param {*} url base URL config
   * @author: nttue (20/07/2023)
   */
  constructor(url) {
    this.url = url;
  }
  /**
   * @description hàm lấy dữ liệu
   * @author: nttue (20/07/2023)
   */
  getById(id) {
    return request.get(this.url + `/${id}`);
  }
  /**
   * @description hàm lấy dữ liệu
   * @author: nttue (20/07/2023)
   */
  get() {
    return request.get(this.url);
  }
  /**
   * @description hàm cập nhật dữ liệu
   * @author: nttue (20/07/2023)
   */
  async update(id, object) {
    return request.put(this.url + id, object);
  }
  /**
   * @description hàm thêm mới dữ liệu
   * @author: nttue (20/07/2023)
   */
  async post(object) {
    return request.post(this.url, object);
  }
  /**
   * @description hàm xóa dữ liệu
   * @author: nttue (20/07/2023)
   */
  async delete(id) {
    return request.delete(this.url + id);
  }

  /**
   * Hàm lọc dữ liệu
   * @param {Object} filterObject Đối tượng lọc dữ liệu
   * @returns Các bản ghi theo kết quả lọc
   * @author: nttue (20/07/2023)
   */
  filterObject(filterObject) {
    return request.post(this.url + "Filter", filterObject);
  }
}

export default BaseAPI;
