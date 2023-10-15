import StoreSection from "./storeSection";

export default class Store {
    constructor(
        public id: string = '',
        public name: string = '',
        public description: string = '',
        public isBlocked: boolean = false,
        public sections: Array<StoreSection> = []
    ) { }
}