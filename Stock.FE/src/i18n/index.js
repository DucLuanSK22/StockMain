import { createI18n } from "vue-i18n";
import Common from "./vi/i18nCommon";
import i18nBooking from "./vi/i18nBooking";

const messages = {
  vi: {
    ...Common,
    i18nBooking: { ...i18nBooking },
  },
};
const i18n = createI18n({
  locale: "vi",
  fallbackLocale: "vi",
  messages,
});
export default i18n;
