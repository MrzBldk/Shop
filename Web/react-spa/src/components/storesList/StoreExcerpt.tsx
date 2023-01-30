import Store from "features/stores/store";
import { Link } from "react-router-dom";
import styles from './StoreExcerpt.module.scss'

export function StoreExcerpt({ store }: { store: Store }) {
    return (
        <article className={styles['stores-list__store-excerpt']}>
            <h3>{store.name}</h3>
            <h4>{store.description}</h4>
            <Link className="button accent-button round-button" to={`/store/${store.id}`}>Store Page</Link>
        </article>
    )
}