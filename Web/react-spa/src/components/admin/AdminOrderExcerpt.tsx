import { formatOrderStatus } from "app/utils";
import { OrderItemExcerpt } from "components/ordersList/OrderItemExcerpt";
import Order from "features/orders/order";
import styles from './AdminOrderExcerpt.module.scss'
import { FormEvent } from 'react'
import orderAPI from "features/orders/orderAPI";
import { useAppDispatch } from "app/hooks";
import { fetchOrdersAsync } from "features/orders/ordersSlice";
import { toast, Toaster } from "react-hot-toast";

export function AdminOrderExcerpt({ order }: { order: Order }) {
    const date = new Date(order.orderDate)
    const dispatch = useAppDispatch()

    const handleClick = async (event: FormEvent<HTMLButtonElement>) => {
        try {
            await orderAPI.changeOrderStatus(order.id, event.currentTarget.name)
            dispatch(fetchOrdersAsync())
            toast.success('Changed order status')
        } catch (err) {
            toast.error(String(err))
        }
    }
    return (
        <article className={styles['admin__order-excerpt']}>
            <h3>Order made on {date.toLocaleString()}</h3>
            <div className={styles.admin__grid}>
                <div>
                    <p>Address: {order.address}</p>
                    <p>Status: {formatOrderStatus(order.status)}</p>
                </div>
                <div>
                    <p>Items:</p>
                    <ul>
                        {order.items.map((item, index) => <OrderItemExcerpt key={index} item={item} />)}
                    </ul>
                </div>
            </div>
            <p>Total Price: {order.price}$</p>
            <button onClick={handleClick} name="awaitingvalidation">
                Awaiting Validation
            </button> <button onClick={handleClick} name="stockconfirmed">
                Stock confirmed
            </button> <button onClick={handleClick} name="shipped">
                Shipped
            </button> <button onClick={handleClick} name='cancelled'>
                Cancelled
            </button>
            <Toaster position="bottom-right" reverseOrder={false} />
        </article>
    )
}