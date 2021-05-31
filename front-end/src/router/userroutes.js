export default [
    {
        path: '/users',
        name: 'users',
        component: () => import('../views/User.vue'),
        meta: {
            requiresAuth: true,
            title: 'Inicio'
        },
        redirect: 'users/list',
        children: [
            {
                path: 'list',
                name: 'list',
                component: () => import('../views/User/List.vue'),
                meta: {
                    requiresAuth: true,
                    display: 'Usuarios',
                    title: 'Usuarios',
                    icon: 'fas fa-users-cog'
                }
            },
            {
                path: 'new',
                name: 'new',
                component: () => import('../views/User/New.vue'),
                meta: {
                    requiresAuth: true,
                    display: 'Crear usuario',
                    title: 'Usuarios',
                    icon: 'fas fa-users-cog'
                }
            },
            {
                path: 'edit',
                name: 'edit',
                component: () => import('../views/User/Edit.vue'),
                meta: {
                    requiresAuth: true,
                    display: 'Editar usuario',
                    title: 'Usuarios',
                    icon: 'fas fa-users-cog'
                }
            },
            {
                path: 'delete',
                name: 'delete',
                component: () => import('../views/User/Delete.vue'),
                meta: {
                    requiresAuth: true,
                    display: 'Eliminar usuario',
                    title: 'Usuarios',
                    icon: 'fas fa-users-cog'
                }
            },
            {
                path: '',
                redirect: 'list'
            }
        ]
    }
]