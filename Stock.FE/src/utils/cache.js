class Cache {
  setCache(key, value, expires = 1000 * 60 * 60 * 1) {
    try {
      localStorage.setItem(key, JSON.stringify(value));
      setTimeout(() => {
        localStorage.removeItem(key);
      }, expires)
    } catch (e) {
      console.log(e);
    }
  }

  getCache(key) {
    return JSON.parse(localStorage.getItem(key));
  }
  deleteCache(key) {
    try {
      localStorage.removeItem(key);
    } catch (e) {
      console.log(e);
    }
  }
}
const cache = new Cache();
export default cache;
