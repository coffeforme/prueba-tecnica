import Vue from 'vue'
import VueRouter from 'vue-router'
import { titlePageRoute, authRoute } from './beforeEachMethods'
import { authRoutes, homeRoutes, userRoutes } from './routes'

Vue.use(VueRouter)

const routes = [
  ...homeRoutes,
  ...authRoutes,
  ...userRoutes,
  {
    path: '/',
    redirect: '/home'
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

const originalPush = VueRouter.prototype.push;
VueRouter.prototype.push = function push(location) {
  return originalPush.call(this, location).catch(err => err);
}
router.beforeEach((to, from, next) => {
  titlePageRoute(to, from, next)
  authRoute(to, from, next)
})


export default router
