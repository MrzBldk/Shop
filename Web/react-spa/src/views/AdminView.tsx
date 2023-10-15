import { useAppDispatch, useAppSelector } from "app/hooks";
import { AdminOrderExcerpt } from "components/admin/AdminOrderExcerpt";
import { fetchOrdersAsync, selectOrders, selectOrdersRepresentativeness, selectOrdersStatus } from "features/orders/ordersSlice";
import { useEffect } from 'react'

export function AdminView() {
    const dispatch = useAppDispatch()
    const orders = useAppSelector(selectOrders)
    const ordersStatus = useAppSelector(selectOrdersStatus)
    const ordersRepresentativeness = useAppSelector(selectOrdersRepresentativeness)

    useEffect(() => {

        async function fetch() {
            dispatch(fetchOrdersAsync())
        }

        if (ordersStatus === 'idle' ||
            (ordersRepresentativeness !== 'all' && ordersStatus !== "failed")) {
            fetch()
        }

    }, [ordersStatus, ordersRepresentativeness, dispatch])

    return (
        <section className="container">
            {orders.map(order => <AdminOrderExcerpt key={order.id} order={order} />)}
        </section>
    )
}