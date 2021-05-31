import PageRoleService from '@/services/parameter/pagerole.service';
import PageRole from '@/models/parameter/pagerole'
import Role from '@/models/auth/role'

const initialState = {
    pagerolelist: [],
    pagerole: {},
    pageRole: new PageRole(),
    role: new Role()
};

export default {
    namespaced: true,
    state: initialState,
    actions: {
        getAll({ commit, dispatch }) {
            return PageRoleService.getAll().then(
                response => {
                    commit('setMenu', response.data, { root: true })
                    return Promise.resolve();
                },
                error => {
                    dispatch('manageError', error, { root: true })
                    return Promise.reject();
                }
            );
        },
        getAllByRole({ state, commit }, uid_role) {
            if (uid_role ?? state.role.uid) {
                return PageRoleService.getAllByRole(uid_role ?? state.role.uid).then(
                    response => {
                        commit('fillList', response.data)
                        return Promise.resolve();
                    },
                    error => {
                        dispatch('manageError', error, { root: true })
                        return Promise.reject();
                    }
                );
            }
        },
        save({ commit, dispatch }, pagerole) {
            return PageRoleService.save(pagerole).then(
                resp => {
                    commit('setMessageFromResp', resp, { root: true })
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
        fillList(state, data) {
            state.pagerolelist = data ?? [];
        },
        setRole(state, data) {
            state.role = data ?? new Role()
        },
        cleanRole(state) {
            state.role = new Role()
        },
    }
};