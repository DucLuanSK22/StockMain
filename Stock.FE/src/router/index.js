import { createRouter, createWebHistory } from "vue-router";
import Cache from "@/utils/cache";
import store from "@/store";
import Home from "@/views/home/Home.vue";
import Login from "@/views/login/Login.vue";
import Signup from "@/views/signup/Signup.vue";
import Asset from "@/views/asset/Asset.vue";
import Purchase from "@/views/purchase/Purchase.vue";
const routes = [
  {
    name: "home",
    component: Home,
    path: "/",
    meta: { requiresAuth: false, title: "Trang chủ" },
  },
  {
    name: "login",
    component: Login,
    path: "/login",
    meta: { requiresAuth: false, title: "Đăng nhập" },
  },
  {
    name: "signup",
    component: Signup,
    path: "/signup",
    meta: { requiresAuth: false, title: "Đăng ký" },
  },
   {
    name: "purchase",
    component: Purchase,
    path: "/purchase",
    meta: { requiresAuth: true, title: "Đặt lệnh" },
  },
   {
    name: "asset",
    component: Asset,
    path: "/asset",
    meta: { requiresAuth: true, title: "Tài sản" },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  let value = Cache.getCache("user");

  function changeTitle(to) {
    if (to.meta.title) {
      document.title = to.meta.title;
    } else {
      document.title = "Hệ thống";
    }
  }
  let auth = false;
  if (value && value.user_id) {
    auth = true;
  }
  if (to.meta.requiresAuth) {
    if (auth) {
      changeTitle(to);
    } else {

      next({ name: "login" });

      store.commit("showToast", {
        label:
          "Vui lòng đăng nhập để sử dụng tính năng!",
        type: "error",
      });
    }
  } else {
    changeTitle(to);
  }

  next(); // Chuyển tiếp để thực hiện điều hướng
});

export default router;
