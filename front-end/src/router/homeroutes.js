export default [
    {
        path: '/home',
        name: 'home',
        component: () => import('../views/Home.vue'),
        meta: {
            requiresAuth: true,
            title: 'Inicio',
            display: 'Home',
        }
    }
]