import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { RootState } from "app/store";
import Type from "./type";
import typeAPI from "./typeAPI";

export interface TypeState {
    value: Array<Type>
    status: 'idle' | 'loading' | 'failed'
}

const initialState: TypeState = {
    value: [],
    status: 'idle'
}

export const fetchTypesAsync = createAsyncThunk(
    'types/fetch',
    async () => {
        const types = await typeAPI.fetchTypes()
        return types
    }
)

export const typesSlice = createSlice({
    name: 'types',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchTypesAsync.pending, (state) => {
                state.status = 'loading'
            })
            .addCase(fetchTypesAsync.fulfilled, (state, action) => {
                state.status = 'idle'
                state.value = action.payload
            })
            .addCase(fetchTypesAsync.rejected, (state) => {
                state.status = 'failed'
            })
    }
})

export const selectTypes = (state: RootState) => state.types.value

export default typesSlice.reducer