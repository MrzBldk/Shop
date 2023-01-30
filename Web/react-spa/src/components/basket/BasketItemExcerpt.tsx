import { getPictureUrl } from "app/utils"
import BasketItem from "features/basket/basketItem"
import styles from './BasketItemExcerpt.module.scss'

export function BasketItemExcerpt({ item }: { item: BasketItem }) {
    return (
        <article className={styles.article}>
            <p>
                <img className={styles.image} src={getPictureUrl(item.pictureUrl)} alt={item.productName} />
                {item.productName} {item.unitPrice}$ x {item.quantity}
            </p>
        </article>
    )
}