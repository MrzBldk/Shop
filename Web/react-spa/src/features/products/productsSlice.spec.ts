import productsReducer, { productsState } from './productsSlice';

describe('products reducer', () => {
    const initialState: productsState = {
        value: [],
        status: 'idle',
    };

    it('should handle initial state', () => {
        expect(productsReducer(undefined, { type: 'unknown' })).toEqual(initialState)
    });
});