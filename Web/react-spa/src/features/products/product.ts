export default class Product {
    constructor(
        public id: string = '', 
        public name: string = '',
        public description: string = '',
        public price: number = 0,
        public picturesUris: Array<string> = [],
        public availableStock: number = 0,
        public brand: string  = '',
        public type: string  = '',
        public isArchived: boolean = false,
        public storeId: string = ''
    ) { }
}