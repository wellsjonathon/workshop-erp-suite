// TODO:
// Create route components (the actual pages) and import them here
// Associate proper components to routes
import Workorders from './components/Workorders/Workorders.vue'
import Workorder from './components/Workorders/Workorder.vue'

export default [
  { path: '/home', component: null },
  {
    name: 'workorders',
    path: '/workorders',
    component: Workorders
  },
  {
    name: 'workorder_by_id',
    path: '/workorders/:workorderId',
    component: Workorder,
    props: true
  },
  { path: '/inventory', component: null },
  { path: '/project-management', component: null },
  { path: '/reports', component: null },
  { path: '/settings', component: null }
]