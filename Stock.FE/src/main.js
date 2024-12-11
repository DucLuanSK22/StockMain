import "@/styles/main.scss";
import router from "@/router";
import store from "@/store";
import Enums from "@/resources/enumeration";
import Icon from "@/components/icons/Icon.vue";
import MButton from "@/components/button/MButton.vue";
import MInputPri from "@/components/input/MInputPri.vue";
import CTable from "@/components/ctable/CTable.vue";
import MSearch from "./components/search/MSearch.vue";
import { createApp } from 'vue';

import App from './App.vue';

const app = createApp(App);
app.config.globalProperties.$route = router;
app.config.globalProperties.$enums = Enums;

app.component("micon", Icon);
app.component("mbutton", MButton);
app.component("minput", MInputPri);
app.component("msearch", MSearch);
app.component("ctable", CTable);

app.use(router).use(store).mount('#app');
