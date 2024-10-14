import { createStore } from "vuex";

import toast from "./modules/toast";
import dialog from "./modules/dialog";
import loading from "./modules/loading";
const store = createStore({
  modules: {
    toast,
    dialog,
    loading,
  },
});

export default store;
