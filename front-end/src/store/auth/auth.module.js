import AuthService from '@/services/auth/auth.service';
import { ENVIROMENT } from '@/config'
import User from "@/models/auth/user";

const token = JSON.parse(localStorage.getItem('token'));
const user = JSON.parse(localStorage.getItem('token'));
const initialState = token
  ? { status: { loggedIn: true }, token, user }
  : { status: { loggedIn: false }, token: null, user: null };

export default {
  namespaced: true,
  state: {
    ...initialState,
    userlist: [],
    user: {}
  },
  actions: {
    getAll({ commit, dispatch }) {
      return AuthService.getAll().then(
        response => {
          commit('filluserlist', response.data);
          return Promise.resolve();
        },
        error => {
          dispatch('manageError', error, { root: true })
          return Promise.reject();
        }
      );
    },
    getData() {

    },
    login({ commit, dispatch }, user) {
      return AuthService.login(user).then(
        resp => {
          if (ENVIROMENT.dev) {
            console.log("Login service called success")
            console.log(resp)
          }

          if (resp.done) {
            commit('clearMessage', null, { root: true })
            commit('loginSuccess', user);
            dispatch('checkauth')
            return Promise.resolve();
          }
          else {
            commit('setMessageFromResp', resp, { root: true })
            commit('loginFailure');
            return Promise.reject();
          }
        },
        error => {
          if (ENVIROMENT.dev)
            console.log("Login failure")
          commit('loginFailure');
          dispatch('manageError', error, { root: true })
          return Promise.reject();
        }
      );
    },
    logout({ commit }) {
      AuthService.logout();
      commit('clearMessage', null, { root: true })
      commit('logout');
    },
    checkauth({ dispatch, commit, state }) {
      if (!state.status.loggedIn) {
        dispatch('logout')
        return Promise.resolve()
      }
      else
        return AuthService.echouser().then(
          resp => {
            if (ENVIROMENT.dev)
              console.log("Is logged service called success")
            if (resp.done) {
              commit('setProfile', resp.data, { root: true })
              return Promise.resolve()
            }
            else
              return dispatch('logout')
          },
          error => {
            if (ENVIROMENT.dev)
              console.log(`Is logged service called failure, ${error}`)
            dispatch('manageError', error, { root: true })
            return dispatch('logout')
          })

    },
    register({ commit, dispatch }, user) {
      return AuthService.register(user).then(
        response => {
          commit('setMessageFromResp', response, { root: true })
          return Promise.resolve();
        },
        error => {
          dispatch('manageError', error, { root: true })
          return Promise.reject();
        }
      );
    },
    update({ commit, dispatch }, user) {
      return AuthService.update(user).then(
        response => {
          commit('setMessageFromResp', response, { root: true })
          return Promise.resolve();
        },
        error => {
          dispatch('manageError', error, { root: true })
          return Promise.reject();
        }
      );
    },
    delete({ commit, dispatch }, user) {
      return AuthService.delete(user).then(
        response => {
          commit('setMessageFromResp', response, { root: true })
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
    loginSuccess(state) {
      state.status.loggedIn = true;
    },
    loginFailure(state) {
      state.status.loggedIn = false;
      state.token = null;
    },
    logout(state) {
      state.status.loggedIn = false;
    },
    filluserlist(state, userlist) {
      state.userlist = userlist ?? [];
    },
    cleanUser(state) {
      state.user = new User()
    },
    setUser(state, data) {
      state.user = data
    }
  }
};
