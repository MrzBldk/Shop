import authService from "app/authService";
import { BasketItemExcerpt } from "components/basket/BasketItemExcerpt";
import { Spinner } from "components/Spinner";
import Basket from "features/basket/basket";
import basketAPI from "features/basket/basketAPI";
import { useEffect, useState } from "react";
import { Toaster, toast } from 'react-hot-toast'

export function BasketView() {

    const [basket, setBasket] = useState<Basket | null>(null)
    const [id, setId] = useState('')

    const totalPrice = basket?.items.reduce(
        (accumulator, item) => accumulator + item.quantity * item.unitPrice,
        0
    )

    useEffect(() => {
        let ignore = false
        const fetch = async () => {
            const userId = await authService.getUserId()
            setId(userId)
            if (!!id) {
                const basket = await basketAPI.fetchBasket(id)
                if (!ignore) setBasket(basket)
            }
        }
        fetch()
        return () => { ignore = true; }
    }, [id])


    const clearBasket = async () => {
        try {
            await basketAPI.clearBasket(basket!.buyerId)
            toast.success('Basket Cleared')
            setBasket(new Basket())
        } catch (err) {
            toast.error(String(err))
        }
    }

    return (
        <section className="container">
            {!basket ?
                <Spinner text="Loading..."></Spinner> :
                basket.items.length === 0 ?
                    <h2 className="text-center">Basket is Empty</h2> :
                    <>
                        {basket.items.map(item => <BasketItemExcerpt key={item.id} item={item} />)}
                        <p>Total Price: {totalPrice}$</p>
                        <button
                            onClick={clearBasket}>
                            Clear Basket
                        </button> <a className="button"
                            href='/makeOrder'>
                            Make an Order
                        </a>
                    </>
            }
            <Toaster
                position="bottom-right"
                reverseOrder={false}
            />
        </section>
    )
}