import brandsReducer, { brandsState } from './brandsSlice';

describe('brands reducer', () => {
    const initialState: brandsState = {
        value: [],
        status: 'idle',
    };

    it('should handle initial state', () => {
        expect(brandsReducer(undefined, { type: 'unknown' })).toEqual(initialState)
    });
});