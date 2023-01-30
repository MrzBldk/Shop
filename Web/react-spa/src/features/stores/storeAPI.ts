import authService from "app/authService";
import Store from "./store";

const URL: string = process.env.REACT_APP_API_URL + '/api/s/store'

async function fetchStores() {
    const responce = await fetch(URL, {
        method: 'GET'
    })

    if (responce.ok) {
        const stores: Array<Store> = (await responce.json()).stores
        return stores;
    } else {
        return Promise.reject(await responce.text());
    }
}

async function fetchStore(id: string) {
    const responce = await fetch(URL + `/${id}`, {
        method: 'GET'
    })

    if (responce.ok) {
        const store: Store = await responce.json()
        return store;
    } else {
        return Promise.reject(await responce.text());
    }
}

async function blockStore(id: string){

    const token = await authService.getToken()

    const responce = await fetch(URL + `/${id}/block`,{
        method: 'PUT',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    })

    if(responce.ok){
        return Promise.resolve()
    } else{
        return Promise.reject(await responce.text())
    }
}

async function unblockStore(id: string){

    const token = await authService.getToken()

    const responce = await fetch(URL + `/${id}/unblock`,{
        method: 'PUT',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    })

    if(responce.ok){
        return Promise.resolve()
    } else{
        return Promise.reject(await responce.text())
    }
}


const storeAPI = { fetchStores, fetchStore, blockStore, unblockStore }

export default storeAPI