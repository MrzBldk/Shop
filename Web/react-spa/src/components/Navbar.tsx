import { Link } from "react-router-dom";
import { LoginMenu } from "./auth/LoginMenu";
import styles from './Navbar.module.scss'

export function Navbar() {
    return (
        <nav className={styles.navbar}>
            <section>
                <h1>Shop</h1>
                <div className={styles.navbar__navContent}>
                    <div className={styles.navbar__navLinks}>
                        <Link to="/">Catalog</Link>
                        <Link to="/stores">Stores</Link>
                        <Link to="/orders">Orders</Link>
                        <Link to="/basket">Basket</Link>
                        <LoginMenu />
                    </div>
                </div>
            </section>
        </nav>
    )
}