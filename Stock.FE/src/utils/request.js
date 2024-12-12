import axios from "axios";
const axiosClient = axios.create({
  baseURL: 'http://150.95.113.231:5023/api/',
  // baseURL: 'https://localhost:7280/api/',
});

axiosClient.interceptors.response.use(
  (res) => {
    if (res && res.data) {
      return res.data;

    }
    return res;
  },
  (error) => {
    const { response } = error;
    const { data } = response;

    return data;
  }
);
export default axiosClient;
