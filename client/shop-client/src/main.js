import Vue from 'vue'
import VueRouter from 'vue-router'
import axios from 'axios'
import App from './App.vue'
import routes from './routes.js'
import './styles/variables.scss'
// import 'bootstrap/dist/css/bootstrap.css'
// import 'bootstrap-vue/dist/bootstrap-vue.css'
import {
  Layout,
  Card,
  Table,
  Alert,
  Form,
  FormTextarea,
  FormGroup,
  FormInput,
  FormSelect,
  FormFile,
  FormCheckbox,
  ListGroup,
  InputGroup,
  Button,
  ButtonGroup,
  ButtonToolbar,
  Dropdown,
  Pagination,
  Tabs,
  Spinner,
  Modal,
  Breadcrumb } from 'bootstrap-vue/es/components'
import { library } from '@fortawesome/fontawesome-svg-core'
import {
  faChevronCircleLeft, faChevronCircleRight,
  faChevronLeft, faChevronRight,
  faChevronDown, faChevronUp,
  faBell,
  faStopwatch,
  faHome,
  faFile,
  faWarehouse,
  faCalendarAlt,
  faChartPie,
  faCog,
  faUser,
  faSearch,
  faPlus,
  faEdit } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
// import Datepicker from 'vuejs-datepicker'

Vue.use(VueRouter)
Vue.use(Layout)
Vue.use(Card)
Vue.use(Table)
Vue.use(Alert)
Vue.use(Form)
Vue.use(FormFile)
Vue.use(FormTextarea)
Vue.use(FormGroup)
Vue.use(FormInput)
Vue.use(FormSelect)
Vue.use(FormCheckbox)
Vue.use(ListGroup)
Vue.use(InputGroup)
Vue.use(Button)
Vue.use(ButtonGroup)
Vue.use(ButtonToolbar)
Vue.use(Breadcrumb)
Vue.use(Dropdown)
Vue.use(Pagination)
Vue.use(Tabs)
Vue.use(Modal)
Vue.use(Spinner)

// Vue.component('datepicker', Datepicker)

Vue.component('FaIcon', FontAwesomeIcon)

// FontAwesome load ins
library.add(faChevronCircleLeft)
library.add(faChevronCircleRight)
library.add(faChevronLeft)
library.add(faChevronRight)
library.add(faChevronDown)
library.add(faChevronUp)
library.add(faBell)
library.add(faStopwatch)
library.add(faHome)
library.add(faFile)
library.add(faWarehouse)
library.add(faCalendarAlt)
library.add(faChartPie)
library.add(faCog)
library.add(faUser)
library.add(faSearch)
library.add(faPlus)
library.add(faEdit)

Vue.config.productionTip = false

axios.defaults.headers.post['Content-type'] = 'application/json'

Vue.prototype.$http = axios

const router = new VueRouter({
  routes
})

new Vue({
  router,
  render: h => h(App),
}).$mount('#app')
