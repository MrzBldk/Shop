import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Store from "features/stores/store"
import storeAPI from "features/stores/storeAPI";
import { StoreProductsList } from "components/store/StoreProductsList";

export function StoreView() {
    let { id } = useParams()
    const [store, setStore] = useState(new Store())

    useEffect(() => {
        let ignore = false
        const fetch = async () => {
            const store = await storeAPI.fetchStore(id!)
            if (!ignore) setStore(store)
        }
        fetch()
        return () => { ignore = true; }
    }, [id])

    return (
        <section className="container">
            <h2 className="text-center">{store.name}</h2>
            <h3 className="text-center">{store.description}</h3>
            <StoreProductsList storeId={id!} />
        </section>
    )
}