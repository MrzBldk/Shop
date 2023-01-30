import { useAppDispatch, useAppSelector } from "app/hooks"
import { fetchProductsAsync, selectProductsByStoreId, selectProductsStatus } from "features/products/productsSlice"
import { useEffect } from "react"
import { ProductExcerpt } from "../catalog/ProductExcerpt"
import { Spinner } from "../Spinner"
import styles from './StoreProductsList.module.scss'

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
                <section className={styles['store__products-list']}>
                    {products.map(p => <ProductExcerpt key={p.id} product={p} />)}
                </section>}
        </>
    )
}