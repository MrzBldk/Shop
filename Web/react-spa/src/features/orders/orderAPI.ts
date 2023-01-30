import authService from "app/authService";
import Order from "./order";

const URL: string = process.env.REACT_APP_API_URL + '/api/o/order'

async function fetchOrders() {

    const token = await authService.getToken()

    const responce = await fetch(URL, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    })

    if (responce.ok) {
        const products: Array<Order> = await responce.json()
        return products;
    } else {
        return Promise.reject(await responce.text());
    }
}

async function fetchUserOrders(userId: string) {

    const token = await authService.getToken()

    const responce = await fetch(URL + `/ByUser/${userId}`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    })

    if (responce.ok) {
        const orders: Array<Order> = await responce.json()
        return orders;
    } else {
        return Promise.reject(await responce.text());
    }
}

async function changeOrderStatus(id: string, status: string) {
    const token = await authService.getToken()
    const responce = await fetch(URL + `/${id}/${status}`, {
        method: 'PUT',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    })

    if (responce.ok) {
        return Promise.resolve()
    } else {
        return Promise.reject(await responce.text())
    }
}

const orderAPI = { fetchOrders, fetchUserOrders, changeOrderStatus }
export default orderAPI