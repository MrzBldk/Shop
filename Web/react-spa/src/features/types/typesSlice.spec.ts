import typesReducer, { TypeState } from './typesSlice';

describe('types reducer', () => {
    const initialState: TypeState = {
        value: [],
        status: 'idle',
    };

    it('should handle initial state', () => {
        expect(typesReducer(undefined, { type: 'unknown' })).toEqual(initialState)
    });
});