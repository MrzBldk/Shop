import Product from "features/products/product";
import { Link } from "react-router-dom";

export function ProductExcerpt({ product }: { product: Product }) {
    return (
        <article>
            <img src={product.picturesUris[0]} alt={product.name} />
            <div>
                <h3>{product.name}</h3>
                <Link to={`/product/${product.id}`}>
                    View Product Info
                </Link>
            </div>
        </article>
    )
}