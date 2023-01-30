import Basket from "./basket";

const URL: string = process.env.REACT_APP_API_URL + '/api/b/basket'

async function fetchBasket(buyerId: string) {
    const responce = await fetch(URL + `/${buyerId}`, {
        method: 'GET'
    })

    if (responce.ok) {
        const basket: Basket = await responce.json()
        return basket;
    } else {
        return Promise.reject(await responce.text());
    }
}

async function clearBasket(buyerId: string) {
    const responce = await fetch(URL + `/${buyerId}`, {
        method: 'DELETE'
    })

    if (responce.ok) {
        return Promise.resolve();
    } else {
        return Promise.reject(await responce.text());
    }
}

const basketAPI = { fetchBasket, clearBasket }

export default basketAPI