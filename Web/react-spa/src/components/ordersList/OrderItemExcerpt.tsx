import OrderItem from "features/orders/orderItem";
import { Link } from "react-router-dom";

export function OrderItemExcerpt({ item }: { item: OrderItem }) {
    return (
        <>
            <li><Link to={`/product/${item.productId}`}>{item.name}</Link> {item.unitPrice}$ x {item.units}</li>
        </>
    )
}