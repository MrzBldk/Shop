import { useAppDispatch, useAppSelector } from "app/hooks"
import { Spinner } from "components/Spinner"
import { fetchProductsAsync, selectProducts, selectProductsStatus } from "features/products/productsSlice"
import { useEffect } from "react"
import { ProductExcerpt } from "./ProductExcerpt"
import styles from './CatalogProductsList.module.scss'

export function CatalogProductsList({ filter, types, brands }: { filter: string, types: Array<string>, brands: Array<string> }) {
    const dispatch = useAppDispatch()
    let products = useAppSelector(selectProducts)
    const productsStatus = useAppSelector(selectProductsStatus)

    useEffect(() => {
        if (productsStatus === 'idle') {
            dispatch(fetchProductsAsync())
        }
    }, [productsStatus, dispatch])


    products = products.filter(p => p.name.toLowerCase().includes(filter.toLowerCase()))
    if (types.length > 0) {
        products = products.filter(p => types.some(t => t === p.type))
    }
    if (brands.length > 0) {
        products = products.filter(p => brands.some(b => b === p.brand))
    }

    return (
        <>
            {productsStatus === 'loading' ?
                <Spinner text="Loading..." /> :
                <section className={styles['catalog__products-list']}>
                    {products.map(p => <ProductExcerpt key={p.id} product={p} />)}
                </section>}
        </>
    )
}