import authService from "app/authService";
import { useAppDispatch, useAppSelector } from "app/hooks";
import { OrderExcerpt } from "components/ordersList/OrderExcerpt";
import { fetchUserOrdersAsync, selectOrders, selectOrdersStatus } from "features/orders/ordersSlice";
import { useEffect } from "react";

export function OrdersListView() {

    const dispatch = useAppDispatch()
    const orders = useAppSelector(selectOrders)
    const ordersStatus = useAppSelector(selectOrdersStatus)

    useEffect(() => {
        
        async function fetch() {
            const userId = await authService.getUserId()
            dispatch(fetchUserOrdersAsync(userId))
        }

        if (ordersStatus === 'idle') {
            fetch()
        }

    }, [ordersStatus, dispatch])


    return (
        <section className="container">
            {orders.map(o => <OrderExcerpt key={o.id} order={o}></OrderExcerpt>)}
        </section>
    )
}