import axios from 'axios';
import authHeader from '../auth-header';
import { API, ENVIROMENT } from '@/config'
import { resolver } from '../resolver.service'

const API_URL = API.ROOT + API.ROLE_PATH;

class RoleService {
    get(id) {
        if (ENVIROMENT.dev)
            console.log("Get one from Role service called")
        return axios.get(API_URL + 'get', { params: { id }, headers: authHeader() }).then(...resolver);
    }

    getSpecific(role) {
        if (ENVIROMENT.dev)
            console.log("Get one from  Role service called")
        return axios.post(API_URL + 'getspecific', role, { headers: authHeader() }).then(...resolver);
    }

    getAll() {
        if (ENVIROMENT.dev)
            console.log("Get all from  Role service called")
        return axios.get(API_URL + 'getall', { headers: authHeader() }).then(...resolver);
    }

    save(role) {
        if (ENVIROMENT.dev) {
            console.log("save Role called")
            console.log(role)
        }
        return axios.post(API_URL + 'save', role, { headers: authHeader() }).then(...resolver);
    }

    drop(role) {
        if (ENVIROMENT.dev) {
            console.log("drop Role called")
            console.log(role)
        }
        return axios.post(API_URL + 'drop', role, { headers: authHeader() }).then(...resolver);
    }
}
export default new RoleService();
