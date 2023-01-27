import { useAppDispatch, useAppSelector } from "app/hooks"
import { fetchProductsAsync, selectProductsByStoreId, selectProductsStatus } from "features/products/productsSlice"
import { useEffect } from "react"
import { ProductExcerpt } from "./ProductExcerpt"
import { Spinner } from "./Spinner"

export function StoreProductsList({storeId} : {storeId: string}) {

    const dispatch = useAppDispatch()
    const products = useAppSelector((state) => selectProductsByStoreId(state, storeId))
    const productsStatus = useAppSelector(selectProductsStatus)

    useEffect(() => {
        if (productsStatus === 'idle') {
            dispatch(fetchProductsAsync())
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