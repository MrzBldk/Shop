import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { RootState } from "app/store";
import Product from "./product";
import ProductAPI, { ProductFilter } from "./productAPI";

export interface productsState {
    value: Array<Product>
    status: 'idle' | 'loading' | 'failed'
}

const initialState: productsState = {
    value: [],
    status: 'idle'
}

export interface fetchProductsParams {
    skip: number,
    take: number,
    filter: ProductFilter | null
}

export const fetchProductsAsync = createAsyncThunk(
    'products/fetch',
    async (data: fetchProductsParams) => {
        const { skip, take, filter } = data
        const products = await ProductAPI.fetchProducts(skip, take, filter)
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
            state.status = 'idle'
            state.value = action.payload
        })
        .addCase(fetchProductsAsync.rejected, (state) => {
            state.status = 'failed'
        })
    }
})

export const selectProducts = (state: RootState) => state.products.value

export default productsSlice.reducer