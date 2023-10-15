export default class OrderItem {
    public constructor(
        public name: string,
        public units: number,
        public unitPrice: number,
        public productId: string
    ){}
}