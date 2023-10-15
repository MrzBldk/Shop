import BasketItem from "./basketItem";

export default class Basket {
    public constructor(
        public buyerId: string = '',
        public items: Array<BasketItem> = []
    ) { }
}