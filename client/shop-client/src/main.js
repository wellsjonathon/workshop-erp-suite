import Vue from 'vue'
import App from './App.vue'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faChevronCircleLeft,
  faChevronCircleRight,
  faHome,
  faFile,
  faWarehouse,
  faCalendarAlt,
  faChartPie,
  faCog } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

// FontAwesome load ins
library.add(faChevronCircleLeft)
library.add(faChevronCircleRight)
library.add(faHome)
library.add(faFile)
library.add(faWarehouse)
library.add(faCalendarAlt)
library.add(faChartPie)
library.add(faCog)

Vue.component('FaIcon', FontAwesomeIcon)

Vue.config.productionTip = false

new Vue({
  render: h => h(App),
}).$mount('#app')
