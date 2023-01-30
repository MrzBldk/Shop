import { useAppDispatch, useAppSelector } from "app/hooks"
import { StoreExcerpt } from "components/storesList/StoreExcerpt"
import { fetchStoresAsync, selectStores, selectStoresStatus } from "features/stores/storesSlice"
import { useEffect } from "react"

export function StoresListView() {

    const dispatch = useAppDispatch()
    const stores = useAppSelector(selectStores)
    const storesStatus = useAppSelector(selectStoresStatus)

    useEffect(() => {
        if (storesStatus === 'idle') {
            dispatch(fetchStoresAsync())
        }
    }, [storesStatus, dispatch])

    return (
        <section className="container">
            {stores.map(s => <StoreExcerpt key={s.id} store={s} />)}
        </section>
    )
}