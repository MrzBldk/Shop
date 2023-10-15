import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { RootState } from "app/store";
import Order from "./order";
import orderAPI from "./orderAPI";

export interface OrderState {
    value: Array<Order>
    status: 'idle' | 'loading' | 'failed' | 'succeeded'
    representativeness: 'none'| 'all'| 'user'
}

const initialState: OrderState = {
    value: [],
    status: 'idle',
    representativeness: 'none'
}

export const fetchOrdersAsync = createAsyncThunk(
    'orders/fetch',
    async () => {
        const orders = orderAPI.fetchOrders()
        return orders
    }
)

export const fetchUserOrdersAsync = createAsyncThunk(
    'orders/fetchByUser',
    async (userId: string) => {
        const orders = orderAPI.fetchUserOrders(userId)
        return orders;
    }
)

export const ordersSlice = createSlice({
    name: 'orders',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchOrdersAsync.pending, (state) => {
                state.status = 'loading'
            })
            .addCase(fetchOrdersAsync.fulfilled, (state, action) => {
                state.status = 'succeeded'
                state.representativeness = 'all'
                state.value = action.payload
            })
            .addCase(fetchOrdersAsync.rejected, (state) => {
                state.status = 'failed'
            })
            .addCase(fetchUserOrdersAsync.pending, (state) => {
                state.status = 'loading'
            })
            .addCase(fetchUserOrdersAsync.fulfilled, (state, action) => {
                state.status = 'succeeded'
                state.representativeness = 'user'
                state.value = action.payload
            })
            .addCase(fetchUserOrdersAsync.rejected, (state) => {
                state.status = 'failed'
            })
    }
})

export const selectOrders = (state: RootState) => state.orders.value
export const selectOrdersStatus = (state: RootState) => state.orders.status
export const selectOrdersRepresentativeness = (state: RootState) => state.orders.representativeness

export default ordersSlice.reducer