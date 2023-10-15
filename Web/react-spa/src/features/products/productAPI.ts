import authService from "app/authService";
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

export interface CreateProductRequest {
    name: string,
    description?: string,
    brandId: string,
    typeId: string,
    price: number,
    availableStock: number,
    storeId: string
}

async function createProduct(product: CreateProductRequest) {
    const token = await authService.getToken()

    const responce = await fetch(URL, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(product)
    })

    if (responce.ok) {
        return (await responce.json()).id
    } else {
        return Promise.reject(await responce.text())
    }
}

const ProductAPI = { fetchProducts, fetchProduct, createProduct }

export default ProductAPI