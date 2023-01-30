import BasketItem from "features/basket/basketItem"

export function BasketItemExcerpt({ item }: { item: BasketItem }) {
    return (
        <article>

            <p>
                <img src={item.pictureUrl} alt={item.productName} />
                {item.productName} {item.unitPrice}$ x {item.quantity}
            </p>
        </article>
    )
}