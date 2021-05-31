import RoleService from '@/services/auth/role.service';

const initialState = {
    rolelist: [],
    role: {}
};

export default {
    namespaced: true,
    state: initialState,
    actions: {
        getAll({ commit, dispatch }) {
            return RoleService.getAll().then(
                response => {
                    commit('fillrolelist', response.data);
                    return Promise.resolve();
                },
                error => {
                    dispatch('manageError', error, { root: true })
                    return Promise.reject();
                }
            );
        }
    },
    mutations: {
        fillrolelist(state, rolelist) {
            state.rolelist = rolelist ?? [];
        }
    }
};
