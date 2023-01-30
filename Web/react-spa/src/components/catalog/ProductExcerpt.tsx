import Product from "features/products/product";
import { Link } from "react-router-dom";
import styles from './ProductExcerpt.module.scss'

export function ProductExcerpt({ product }: { product: Product }) {
    return (
        <article className={styles['product-excerpt']}>
            <img src={product.picturesUris[0]} alt={product.name} />
            <div>
                <h4>{product.name}</h4>
                <p>Price: {product.price}$</p>
                <Link className="button" to={`/product/${product.id}`}>
                    View Product Info
                </Link>
            </div>
        </article>
    )
}