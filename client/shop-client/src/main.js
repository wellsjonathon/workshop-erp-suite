import Vue from 'vue'
import VueRouter from 'vue-router'
import axios from 'axios'
import App from './App.vue'
import routes from './routes.js'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import {
  Layout,
  Card,
  Table,
  Form,
  FormInput,
  FormSelect,
  InputGroup,
  Button,
  ButtonGroup,
  ButtonToolbar,
  Dropdown,
  Pagination } from 'bootstrap-vue/es/components'
import BBreadcrumb from 'bootstrap-vue/es/components/breadcrumb/breadcrumb'
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
  faSearch,
  faPlus } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

Vue.use(VueRouter)
Vue.use(Layout)
Vue.use(Card)
Vue.use(Table)
Vue.use(Form)
Vue.use(FormInput)
Vue.use(FormSelect)
Vue.use(InputGroup)
Vue.use(Button)
Vue.use(ButtonGroup)
Vue.use(ButtonToolbar)
Vue.use(Dropdown)
Vue.use(Pagination)

Vue.component('FaIcon', FontAwesomeIcon)
Vue.component('b-breadcrumb', BBreadcrumb)

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
library.add(faPlus)

Vue.config.productionTip = false
Vue.prototype.$http = axios

const router = new VueRouter({
  routes
})

new Vue({
  router,
  render: h => h(App),
}).$mount('#app')
