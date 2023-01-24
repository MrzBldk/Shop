import { objToQueryString } from "app/utils";
import Product from "./product";

const URL: string = process.env.REACT_APP_API_URL + '/api/product'

export interface ProductFilter {
    brands: Array<string>,
    types: Array<string>,
    stores: Array<string>
}

async function fetchProducts(skip: number = 0, take: number = 10, filter: ProductFilter | null = null) {
    
    let params = objToQueryString({skip, take, ...filter}) 
    
    const responce = await fetch(URL + params, {
        method: 'GET'
    });

    if (responce.ok) {
        const products: Array<Product> = await responce.json()
        return products
    } else {
        return Promise.reject(await responce.text());
    }
}

const ProductAPI = {fetchProducts}

export default ProductAPI