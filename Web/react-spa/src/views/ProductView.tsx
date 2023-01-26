import Product from "features/products/product"
import ProductAPI from "features/products/productAPI"
import { useEffect, useState } from "react"
import { Link, useParams } from "react-router-dom"

export function ProductView() {
    let { id } = useParams()

    const [product, setProduct] = useState(new Product())

    useEffect(() => {
        let ignore = false
        const fetch = async () => {
            const product = await ProductAPI.fetchProduct(id!)
            if (!ignore) setProduct(product)
        }
        fetch()
        return () => { ignore = true; }
    }, [id])


    return (
        <>
            <p>Name: {product.name}</p>
            <p>Description: {product.description}</p>
            <p>Price: {product.price}</p>
            <p>Brand: {product.brand}</p>
            <p>Type: {product.type}</p>
            <p>Available: {product.availableStock}</p>
            <Link to={`/store/${product.storeId}`}>Go to store</Link>
        </>
    )
}