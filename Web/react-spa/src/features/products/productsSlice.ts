import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { RootState } from "app/store";
import Product from "./product";
import ProductAPI from "./productAPI";

export interface productsState {
    value: Array<Product>
    status: 'idle' | 'loading' | 'failed' | 'succeeded'
}

const initialState: productsState = {
    value: [],
    status: 'idle'
}

export const fetchProductsAsync = createAsyncThunk(
    'products/fetch',
    async () => {
        const products = await ProductAPI.fetchProducts()
        return products;
    }
)

export const productsSlice = createSlice({
    name: 'products',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchProductsAsync.pending, (state) => {
                state.status = 'loading'
            })
            .addCase(fetchProductsAsync.fulfilled, (state, action) => {
                state.status = 'succeeded'
                state.value = action.payload
            })
            .addCase(fetchProductsAsync.rejected, (state) => {
                state.status = 'failed'
            })
    }
})

export const selectProductsStatus = (state: RootState) => state.products.status
export const selectProducts = (state: RootState) => state.products.value
export const selectProductById = (state: RootState, id: string) => state.products.value.find(p => p.id === id)
export const selectProductsByStoreId = (state: RootState, storeId: string) =>
    state.products.value.filter(p => p.storeId === storeId)

export default productsSlice.reducer