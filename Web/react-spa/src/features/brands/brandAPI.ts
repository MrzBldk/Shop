import Brand from "./brand";

const URL: string = process.env.REACT_APP_API_URL + '/api/brand'

async function fetchBrands() {
    const responce = await fetch(URL, {
        method: 'GET'
    });

    if (responce.ok) {
        const products: Array<Brand> = await responce.json()
        return products;
    } else {
        return Promise.reject(await responce.text());
    }
}

const brandAPI = { fetchBrands }

export default brandAPI