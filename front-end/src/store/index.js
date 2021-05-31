import Vue from 'vue';
import Vuex from 'vuex';

import { authstore } from './auth';
import { parameterstore } from './params'
import Message from '@/models/message';
import User from '../models/auth/user';

Vue.use(Vuex);

const menu = JSON.parse(localStorage.getItem('menu')) ?? [];

const state = {
  profile: {},
  user: {},
  messages: [],
  actions: [],
  alert: false,
  loading: false,
  menu
}


const clearMessage = 5000 //En milisegundos
export default new Vuex.Store({
  modules: {
    ...authstore,
    ...parameterstore
  },
  state: state,
  mutations: {
    setMessageFromResp(state, resp) {
      state.messages = []
      state.messages.push(new Message(resp.response, resp.status, true))
      setTimeout(() => {
        //Limpia el mensaje
        state.messages.pop(0)
      }, clearMessage);
    },
    setMessage(state, msg) {
      state.messages = []
      state.messages.push(new Message(msg, 'info', true))
      setTimeout(() => {
        //Limpia el mensaje
        state.messages.pop(0)
      }, clearMessage);
    },
    clearMessage(state) {
      state.messages = []
    },
    setLoading(state, val) {
      state.loading = val
    },
    setMenu(state, data) {
      localStorage.setItem('menu', JSON.stringify(data));
      //Si data es null inicializa una lista vacia
      state.menu = data ?? []
    },
    setActions(state, data) {
      state.actions = data ?? []
    },
    clearUser(state) {
      state.user = new User()
    },
    setProfile(state, data) {
      state.profile = data
    }
  },
  actions: {
    manageError({ dispatch, commit }, error) {
      if (error.toString().includes("401")) {
        commit("clearMessage")
        commit('setMessage', "La sesión expiró por favor ingrese de nuevo")
        dispatch("auth/logout")
      }
      if (error.toString().includes("Network Error")) {
        commit('setMessage', "Imposible conectar con el servidor, intente más tarde.")
      }
    }
  }
});