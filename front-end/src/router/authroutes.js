export default [
    {
        path: '/login',
        name: 'login',
        component: () => import('../views/Login.vue'),
        meta: {
            display: 'Inicio de sesión',
            title: 'Inicio de sesión'
        }
    }
]