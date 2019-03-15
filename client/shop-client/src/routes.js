// TODO:
// Create route components (the actual pages) and import them here
// Associate proper components to routes
import Workorders from './components/Workorders/Workorders.vue'
import Workorder from './components/Workorders/Workorder.vue'
import NewWorkorder from './components/Workorders/NewWorkorder.vue'
import Materials from './components/Inventory/Materials/Materials.vue'
import Material from './components/Inventory/Materials/Material.vue'
import NewMaterial from './components/Inventory/Materials/NewMaterial.vue'

export default [
  {
    name: 'home',
    path: '/home',
    component: null
  },
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
  {
    name: 'new_workorder',
    path: '/workorders/new',
    component: NewWorkorder
  },
  {
    name: 'materials',
    path: '/materials',
    component: Materials
  },
  {
    name: 'material_by_id',
    path: '/materials/:materialId',
    component: Material,
    props: true
  },
  {
    name: 'new_material',
    path: '/materials/new',
    component: NewMaterial
  },
  { path: '/project-management', component: null },
  { path: '/reports', component: null },
  { path: '/settings', component: null }
]