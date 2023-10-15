export default class BasketItem {
    constructor(
        public id: string,
        public productName: string,
        public unitPrice: number,
        public quantity: number,
        public pictureUrl: string,
        public productId: string
    ) { }
}