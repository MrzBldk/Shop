import Order from "./order";

const URL: string = process.env.REACT_APP_API_URL + '/api/o/order'

async function fetchOrders() {
    const responce = await fetch(URL, {
        method: 'GET'
    })

    if (responce.ok) {
        const products: Array<Order> = await responce.json()
        return products;
    } else {
        return Promise.reject(await responce.text());
    }
}

async function fetchUserOrders(userId: string) {
    const responce = await fetch(URL + `/ByUser/${userId}`, {
        method: 'GET'
    })

    if (responce.ok) {
        const orders: Array<Order> = await responce.json()
        return orders;
    } else {
        return Promise.reject(await responce.text());
    }
}

const orderAPI = {fetchOrders, fetchUserOrders}
export default orderAPI