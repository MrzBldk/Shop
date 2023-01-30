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

async function fetchManagedStore() {

    const token = await authService.getToken()

    const responce = await fetch(URL + '/managed',{
        method: 'GET',
        headers: {
            'Authorization' : `Bearer ${token}`
        }
    })

    if(responce.ok){
        const store: Store = await responce.json()
        return store
    } else if(responce.status === 404){
        return null
    } else {
        return Promise.reject(await responce.json())
    }
    
}

async function createStore(name: string, description?: string){
    const token = await authService.getToken()
    const responce = await fetch(URL,{
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            name,
            description
        })
    })

    if(responce.ok){
        return Promise.resolve()
    } else{
        console.log(await responce.text())
        return Promise.reject(await responce.text())
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


const storeAPI = { fetchStores, fetchStore, fetchManagedStore, blockStore, unblockStore, createStore }

export default storeAPI