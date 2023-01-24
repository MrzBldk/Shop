import Store from "./store";

const URL: string = process.env.REACT_APP_API_URL + '/api/store'

async function fetchStores() {
    const responce = await fetch(URL, {
        method: 'GET'
    });

    if (responce.ok) {
        const products: Array<Store> = await responce.json()
        return products;
    } else {
        return Promise.reject(await responce.text());
    }
}

const storeAPI = { fetchStores }

export default storeAPI