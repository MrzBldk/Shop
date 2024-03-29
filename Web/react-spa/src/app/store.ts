import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import productsReducer from 'features/products/productsSlice'
import brandsReducer from 'features/brands/brandsSlice'
import typesReducer from 'features/types/typesSlice'
import storesReducer from 'features/stores/storesSlice'
import ordersReducer from 'features/orders/ordersSlice'

export const store = configureStore({
    reducer: {
        products: productsReducer,
        brands: brandsReducer,
        types: typesReducer,
        stores: storesReducer,
        orders: ordersReducer,
    },
})

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
    ReturnType,
    RootState,
    unknown,
    Action<string>
>
