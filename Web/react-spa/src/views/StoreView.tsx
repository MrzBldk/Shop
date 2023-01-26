import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Store from "features/stores/store"
import storeAPI from "features/stores/storeAPI";
import { StoreProductsList } from "components/StoreProductsList";

export function StoreView() {
    let { id } = useParams()
    const [store, setStore] = useState(new Store())

    useEffect(() => {
        let ignore = false
        const fetch = async () => {
            const product = await storeAPI.fetchStore(id!)
            if (!ignore) setStore(product)
        }
        fetch()
        return () => { ignore = true; }
    }, [id])

    return (
        <>
            <h1>{store.name}</h1>
            <h2>{store.description}</h2>
            <StoreProductsList />
        </>
    )
}