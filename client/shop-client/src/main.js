import Vue from 'vue'
import VueRouter from 'vue-router'
import axios from 'axios'
import App from './App.vue'
import routes from './routes.js'
import { library } from '@fortawesome/fontawesome-svg-core'
import {
  faChevronCircleLeft, faChevronCircleRight,
  faChevronLeft, faChevronRight,
  faChevronDown, faChevronUp,
  faHome,
  faFile,
  faWarehouse,
  faCalendarAlt,
  faChartPie,
  faCog,
  faUser,
  faSearch } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

Vue.use(VueRouter)

// FontAwesome load ins
library.add(faChevronCircleLeft)
library.add(faChevronCircleRight)
library.add(faChevronLeft)
library.add(faChevronRight)
library.add(faChevronDown)
library.add(faChevronUp)
library.add(faHome)
library.add(faFile)
library.add(faWarehouse)
library.add(faCalendarAlt)
library.add(faChartPie)
library.add(faCog)
library.add(faUser)
library.add(faSearch)

Vue.component('FaIcon', FontAwesomeIcon)

Vue.config.productionTip = false
Vue.prototype.$http = axios

const router = new VueRouter({
  routes
})

new Vue({
  router,
  render: h => h(App),
}).$mount('#app')
