import axios from 'axios';
import authHeader from '../auth-header';
import { API, ENVIROMENT } from '@/config'
import router from '@/router'
import { resolver } from '../resolver.service'

const API_URL = API.ROOT + API.AUTH_PATH;

class AuthService {
    getAll() {
        if (ENVIROMENT.dev)
            console.log("Get all users service called")
        return axios.get(API_URL + 'getall', { headers: authHeader() }).then(...resolver);
    }

    login(user) {
        if (ENVIROMENT.dev)
            console.log("Login service called")
        return axios
            .post(API_URL + 'signin', user)
            .then(response => {
                if (response.data.done) {
                    localStorage.setItem('token', JSON.stringify(response.data));
                }

                return response.data;
            });
    }
    //Retorna si hay una sesion abierta
    echouser() {
        if (ENVIROMENT.dev)
            console.log("Echo user is called")

        return axios.get(API_URL + 'EchoUser', { headers: authHeader() }).then(...resolver);
    }

    logout() {
        if (ENVIROMENT.dev)
            console.log("logout service called")
        localStorage.removeItem('token');
        localStorage.removeItem('menu')
        router.push({ name: 'login' })
    }

    register(user) {
        if (ENVIROMENT.dev)
            console.log("register service called")
        return axios.post(API_URL + 'signup', user, { headers: authHeader() }).then(...resolver);
    }

    update(user) {
        if (ENVIROMENT.dev)
            console.log("update service called")
        return axios.post(API_URL + 'update', user, { headers: authHeader() }).then(...resolver);
    }

    delete(user) {
        if (ENVIROMENT.dev)
            console.log("register delete called")
        return axios.post(API_URL + 'delete', user, { headers: authHeader() }).then(...resolver);
    }
}

export default new AuthService();