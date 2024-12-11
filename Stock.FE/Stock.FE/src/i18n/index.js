import { createI18n } from "vue-i18n";
import Common from "./vi/i18nCommon";
import i18nEnum from "./vi/i18nEnum";

const messages = {
  vi: {
    ...Common,
    i18nEnum: { ...i18nEnum },
  },
};
const i18n = createI18n({
  locale: "vi",
  fallbackLocale: "vi",
  messages,
});
export default i18n;
