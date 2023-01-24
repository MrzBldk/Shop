import StoreSection from "./storeSection";

export default class Store {
    constructor(
        public id: string,
        public name: string,
        public description: string,
        public isBlocked: boolean,
        public sections: Array<StoreSection>
    ) { }
}