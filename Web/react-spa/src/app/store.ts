import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import counterReducer from 'features/counter/counterSlice';
import productsReducer from 'features/products/productsSlice'
import brandsReducer from 'features/brands/brandsSlice'
import typesReducer from 'features/types/typesSlice'
import storesReducer from 'features/stores/storesSlice'

export const store = configureStore({
  reducer: {
    counter: counterReducer,
    products: productsReducer,
    brands: brandsReducer,
    types: typesReducer,
    stores: storesReducer
  },
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
