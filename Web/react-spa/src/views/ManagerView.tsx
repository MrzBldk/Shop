import { CreateProductForm } from "components/manager/CreateProductForm";
import { CreateStoreForm } from "components/manager/CreateStoreForm";
import { Spinner } from "components/Spinner";
import Store from "features/stores/store";
import storeAPI from "features/stores/storeAPI";
import { useEffect, useState } from "react";

export function ManagerView() {

    const [store, setStore] = useState<Store | null>()

    useEffect(() => {
        let ignore = false
        const fetch = async () => {
            const store = await storeAPI.fetchManagedStore()
            if (!ignore) setStore(store)
        }
        fetch()
        return () => { ignore = true }
    })

    return (
        <section className="small-container">
            {typeof store === 'undefined' ?
                <Spinner text="Loading..." /> :
                !store ?
                    <CreateStoreForm /> :
                    <CreateProductForm storeId={store.id} />}
        </section>
    )
}