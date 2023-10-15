import OrderItem from "./orderItem";

export default class Order {
    constructor(
        public id: string,
        public address: string,
        public orderDate: string,
        public price: number,
        public status: string,
        public userId: string,
        public items: Array<OrderItem>
    ) { }
}