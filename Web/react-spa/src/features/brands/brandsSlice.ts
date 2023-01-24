import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { RootState } from "app/store";
import Brand from "./brand";
import brandAPI from "./brandAPI";

export interface brandsState {
    value: Array<Brand>
    status: 'idle' | 'loading' | 'failed'
}

const initialState: brandsState = {
    value: [],
    status: 'idle'
}

export const fetchBrandsAsync = createAsyncThunk(
    'brands/fetch',
    async () => {
        const brands = await brandAPI.fetchBrands()
        return brands;
    }
)

export const brandsSlice = createSlice({
    name: 'brands',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchBrandsAsync.pending, (state) => {
                state.status = 'loading'
            })
            .addCase(fetchBrandsAsync.fulfilled, (state, action) => {
                state.status = 'idle'
                state.value = action.payload
            })
            .addCase(fetchBrandsAsync.rejected, (state) => {
                state.status = 'failed'
            })
    }
})

export const selectBrands = (state: RootState) => state.brands.value

export default brandsSlice.reducer;