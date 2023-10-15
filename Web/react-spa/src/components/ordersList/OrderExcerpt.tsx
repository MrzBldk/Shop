import Order from "features/orders/order";
import { OrderItemExcerpt } from "./OrderItemExcerpt";
import styles from './OrderExcerpt.module.scss'
import { formatOrderStatus } from "app/utils";

export function OrderExcerpt({ order }: { order: Order }) {
    const date = new Date(order.orderDate)
    return (
        <article className={styles['orders-list__order-excerpt']}>
            <h3>Order made on {date.toLocaleString()}</h3>
            <div className={styles['orders-list__grid']}>
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
        </article>
    )
}