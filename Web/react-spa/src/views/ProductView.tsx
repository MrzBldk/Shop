import authService from "app/authService"
import { ImageSlider } from "components/product/ImageSlider"
import Product from "features/products/product"
import ProductAPI from "features/products/productAPI"
import { useEffect, useState } from "react"
import { Toaster, toast } from "react-hot-toast"
import { Link, useNavigate, useParams } from "react-router-dom"

export function ProductView() {
    const { id } = useParams()
    const navigate = useNavigate()

    const [product, setProduct] = useState(new Product())
    const isAvailble = product.availableStock > 0 && !product.isArchived
    useEffect(() => {
        let ignore = false
        const fetch = async () => {
            const product = await ProductAPI.fetchProduct(id!)
            if (!ignore) setProduct(product)
        }
        fetch()
        return () => { ignore = true; }
    }, [id])


    const handleButtonClick = async () => {

        const basketId = await authService.getUserId()

        console.log(basketId)
        if (!basketId) navigate(`/login?returnUrl=product/${id}`)

        const responce = await fetch(process.env.REACT_APP_API_URL + '/api/agg/basket', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ catalogItemId: product.id, quantity: 1, basketId })
        })

        if (responce.ok) {
            toast.success('Added to Basket!')
        } else {
            toast.error(await responce.text())
        }
    }

    return (
        <section className="container">
            <ImageSlider slides={product.picturesUris} />
            <h2>{product.name}</h2>
            <p>{product.type} from {product.brand}, <b><big>{product.price}$</big></b></p>
            <p>{product.description}</p>
            <Link className="button accent-button"
                to={`/store/${product.storeId}`}>
                Go to Store
            </Link> <button
                disabled={!isAvailble}
                className={isAvailble ? 'button' : 'muted-button'}
                onClick={handleButtonClick}>
                Add to Basket
            </button>
            <Toaster
                position="bottom-right"
                reverseOrder={false}
            />
        </section>
    )
}