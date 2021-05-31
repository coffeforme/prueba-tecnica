import Vue from 'vue';
import Vuetify from 'vuetify';
import 'vuetify/dist/vuetify.min.css';
import es from 'vuetify/es5/locale/es';
import { ENVIROMENT } from '@/config'

Vue.use(Vuetify);

export default new Vuetify({
  theme: {
    dark: ENVIROMENT.dark,
    options: {
      customProperties: true,
      themeCache: {
        get: key => localStorage.getItem(key),
        set: (key, value) => localStorage.setItem(key, value),
      }
    },
    themes: {
      light: {
        primary: '#3f51b5',
        secondary: '#2196f3',
        accent: '#00bcd4',
        error: '#f44336',
        warning: '#ffc107',
        info: '#ff9800',
        success: '#009688',
        background: '#e9eaf0'
      },
      dark: {
        background: '#323b57'

      }
    },
  },
  lang: {
    locales: { es },
    current: 'es',
  },
  icons: {
    iconfont: 'fa',
  },
});
