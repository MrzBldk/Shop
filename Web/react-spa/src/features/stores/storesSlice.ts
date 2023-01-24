import { createAsyncThunk, createSlice } from "@reduxjs/toolkit"
import { RootState } from "app/store"
import Store from "./store"
import storeAPI from "./storeAPI"

export interface StoreState {
    value: Array<Store>
    status: 'idle' | 'loading' | 'failed'
}

const initialState: StoreState = {
    value: [],
    status: 'idle'
}

export const fetchStoresAsync = createAsyncThunk(
    'stores/fetch',
    async () => {
        const stores = await storeAPI.fetchStores();
        return stores;
    }
)

export const storesSlice = createSlice({
    name: 'stores',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchStoresAsync.pending, (state) => {
                state.status = 'loading'
            })
            .addCase(fetchStoresAsync.fulfilled, (state, action) => {
                state.status = 'idle'
                state.value = action.payload
            })
            .addCase(fetchStoresAsync.rejected, (state) => {
                state.status = 'failed'
            })
    }
})

export const selectStores = (state: RootState) => state.stores.value

export default storesSlice.reducer