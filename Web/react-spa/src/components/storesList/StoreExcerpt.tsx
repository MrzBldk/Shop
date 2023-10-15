import authService from "app/authService";
import { useAppDispatch } from "app/hooks";
import { fetchProductsAsync } from "features/products/productsSlice";
import Store from "features/stores/store";
import storeAPI from "features/stores/storeAPI";
import { fetchStoresAsync } from "features/stores/storesSlice";
import { useEffect, useState } from "react";
import { toast, Toaster } from "react-hot-toast";
import { Link } from "react-router-dom";
import styles from './StoreExcerpt.module.scss'

export function StoreExcerpt({ store }: { store: Store }) {

    const [isAdmin, setIsAdmin] = useState(false)
    const dispatch = useAppDispatch()

    useEffect(() => {
        const populateState = async () => {
            setIsAdmin(await authService.getUserRole() === 'ShopAdmin')
        }
        populateState()
    })


    const handleClick = async () => {
        try {
            if (store.isBlocked) {
                await storeAPI.unblockStore(store.id)
                toast.success('Store unblocked')
            } else {
                await storeAPI.blockStore(store.id)
                toast.success('Store blocked')
            }
            dispatch(fetchStoresAsync())
            dispatch(fetchProductsAsync())
        } catch (err) {
            toast.error(String(err))
        }
    }

    return (
        <article className={styles['stores-list__store-excerpt']}>
            <h3>{store.name}</h3>
            <h4>{store.description}</h4>
            <Link className="button accent-button round-button" to={`/store/${store.id}`}>Store Page</Link>
            {isAdmin ?
                <>
                    <button className="round-button" onClick={handleClick}>
                        {store.isBlocked ? 'Unblock store' : 'Block Store'}
                    </button>
                    <Toaster position="bottom-right" reverseOrder={false} />
                </> :
                null
            }
        </article>
    )
}