import { useAppDispatch, useAppSelector } from "app/hooks"
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
        <>
            {stores.map(s => <p key={s.id}>{s.name}</p>)}
        </>
    )
}