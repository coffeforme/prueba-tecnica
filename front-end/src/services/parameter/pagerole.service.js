import axios from 'axios';
import authHeader from '../auth-header';
import { API, ENVIROMENT } from '@/config'
import { resolver } from '../resolver.service'

const API_URL = API.ROOT + API.PAGEROLE_PATH;

class PageRoleService {
    get(id) {
        if (ENVIROMENT.dev)
            console.log("Get one from Pagerole service called")
        return axios.get(API_URL + 'get', { params: { id }, headers: authHeader() }).then(...resolver);
    }

    getSpecific(pagerole) {
        if (ENVIROMENT.dev)
            console.log("Get one from  Pagerole service called")
        return axios.post(API_URL + 'getspecific', pagerole, { headers: authHeader() }).then(...resolver);
    }

    getAll() {
        if (ENVIROMENT.dev)
            console.log("Get all from  Pagerole service called")
        return axios.get(API_URL + 'getall', { headers: authHeader() }).then(...resolver);
    }

    getAllByRole(uid_role) {
        if (ENVIROMENT.dev)
            console.log("Get all by role from  Pagerole service called")
        return axios.get(API_URL + 'getallbyrole',{ params: { uid_role },  headers: authHeader() }).then(...resolver);
    }
    

    save(pagerole) {
        if (ENVIROMENT.dev) {
            console.log("save Pagerole called")
            console.log(pagerole)
        }
        return axios.post(API_URL + 'save', pagerole, { headers: authHeader() }).then(...resolver);
    }

    drop(pagerole) {
        if (ENVIROMENT.dev) {
            console.log("drop Pagerole called")
            console.log(pagerole)
        }
        return axios.post(API_URL + 'drop', pagerole, { headers: authHeader() }).then(...resolver);
    }
}
export default new PageRoleService();
