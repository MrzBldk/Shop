import ordersReducer, { OrderState } from './ordersSlice';

describe('orders reducer', () => {
    const initialState: OrderState = {
        value: [],
        status: 'idle',
        representativeness: 'none'
    };

    it('should handle initial state', () => {
        expect(ordersReducer(undefined, { type: 'unknown' })).toEqual(initialState)
    });
});