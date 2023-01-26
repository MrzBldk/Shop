import Type from "./type";

const URL: string = process.env.REACT_APP_API_URL + '/api/c/type'

async function fetchTypes() {
    const responce = await fetch(URL, {
        method: 'GET'
    });

    if (responce.ok) {
        const types: Array<Type> = await responce.json()
        return types;
    } else {
        return Promise.reject(await responce.text());
    }
}

const typeAPI = { fetchTypes }

export default typeAPI