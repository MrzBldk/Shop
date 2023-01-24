export default class Product {
    constructor(
        public id: string,
        public name: string,
        public description: string,
        public price: number,
        public picturesUris: Array<string>,
        public availableStock: number,
        public brand: string,
        public type: string,
        public isArchived: boolean,
        public storeId: string
    ) { }
}