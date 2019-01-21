// TODO:
// Create route components (the actual pages) and import them here
// Associate proper components to routes
import Workorders from './components/Workorders/Workorders.vue'

export default [
  { path: '/dashboard', component: null },
  { path: '/workorders', component: Workorders },
  { path: '/inventory', component: null },
  { path: '/project-management', component: null },
  { path: '/reports', component: null },
  { path: '/settings', component: null }
]