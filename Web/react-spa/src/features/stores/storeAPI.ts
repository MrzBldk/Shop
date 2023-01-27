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

const storeAPI = { fetchStores, fetchStore }

export default storeAPI