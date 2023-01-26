import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { RootState } from "app/store";
import Basket from "./basket";
import basketAPI from "./basketAPI";

export interface BasketState {
    value: Basket | null
    status: 'idle' | 'loading' | 'failed'
}

const initialState: BasketState = {
    value: null,
    status: 'idle'
}

export const fetchBasketAsync = createAsyncThunk(
    'basket/fetch',
    async (buyerId: string) => {
        const basket = basketAPI.fetchBasket(buyerId)
        return basket
    }
)

export const basketSlice = createSlice({
    name: 'basket',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchBasketAsync.pending, (state) => {
                state.status = 'loading'
            })
            .addCase(fetchBasketAsync.fulfilled, (state, action) => {
                state.status = 'idle'
                state.value = action.payload
            })
            .addCase(fetchBasketAsync.rejected, (state) => {
                state.status = 'failed'
            })
    }
})

export const selectBasket = (state: RootState) => state.basket.value

export default basketSlice.reducer