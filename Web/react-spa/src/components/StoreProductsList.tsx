import { useAppDispatch, useAppSelector } from "app/hooks"
import { fetchProductsAsync, selectProducts, selectProductsStatus } from "features/products/productsSlice"
import { useEffect } from "react"
import { ProductExcerpt } from "./ProductExcerpt"
import { Spinner } from "./Spinner"

export function StoreProductsList() {

    const dispatch = useAppDispatch()
    let products = useAppSelector(selectProducts)
    const productsStatus = useAppSelector(selectProductsStatus)

    useEffect(() => {
        if (productsStatus === 'idle') {
            dispatch(fetchProductsAsync({ skip: 0, take: 10 }))
        }
    }, [productsStatus, dispatch])

    return (
        <>
            {productsStatus === 'loading' ?
                <Spinner text="Loading..." /> :
                products.map(p => <ProductExcerpt key={p.id} product={p} />)}
        </>
    )
}