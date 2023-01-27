import Product from "./product";

const URL: string = process.env.REACT_APP_API_URL + '/api/c/product'

export interface ProductFilter {
    brands?: Array<string>,
    types?: Array<string>,
    stores?: Array<string>
}

async function fetchProduct(id: string) {
        
    const responce = await fetch(URL + `/${id}`, {
        method: 'GET'
    });

    if (responce.ok) {
        const product: Product = await responce.json()
        return product
    } else {
        return Promise.reject(await responce.text());
    }
}

async function fetchProducts() {    
    const responce = await fetch(URL, {
        method: 'GET'
    });

    if (responce.ok) {
        const products: Array<Product> = await responce.json()
        return products
    } else {
        return Promise.reject(await responce.text());
    }
}

const ProductAPI = {fetchProducts, fetchProduct}

export default ProductAPI