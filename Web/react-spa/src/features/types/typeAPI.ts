import Type from "./type";

const URL: string = process.env.REACT_APP_API_URL + '/api/type'

async function fetchTypes() {
    const responce = await fetch(URL, {
        method: 'GET'
    });

    if (responce.ok) {
        const products: Array<Type> = await responce.json()
        return products;
    } else {
        return Promise.reject(await responce.text());
    }
}

const typeAPI = { fetchTypes }

export default typeAPI