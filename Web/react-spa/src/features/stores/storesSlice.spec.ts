import storesReducer, { StoreState } from './storesSlice';

describe('stores reducer', () => {
    const initialState: StoreState = {
        value: [],
        status: 'idle',
    };

    it('should handle initial state', () => {
        expect(storesReducer(undefined, { type: 'unknown' })).toEqual(initialState)
    });
});