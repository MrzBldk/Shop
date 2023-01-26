import Brand from "./brand";

const URL: string = process.env.REACT_APP_API_URL + '/api/c/brand'

async function fetchBrands() {
    const responce = await fetch(URL, {
        method: 'GET'
    });

    if (responce.ok) {
        const brands: Array<Brand> = await responce.json()
        return brands;
    } else {
        return Promise.reject(await responce.text());
    }
}

const brandAPI = { fetchBrands }

export default brandAPI