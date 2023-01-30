import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Counter } from 'features/counter/Counter';
import { CatalogView } from 'views/CatalogView';
import { ProductView } from 'views/ProductView';
import { StoreView } from 'views/StoreView';
import { Login } from 'components/auth/Login';
import { StoresListView } from 'views/StoresListView';
import { Navbar } from 'components/Navbar';
import { Logout } from 'components/auth/Logout';
import { OrdersListView } from 'views/OrdersListView';
import { BasketView } from 'views/BasketView';
import { MakeOrderView } from 'views/MakeOrderView';
function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes>
        <Route path='/' element={<CatalogView />} />
        <Route path='/product/:id' element={<ProductView />} />
        <Route path='/stores' element={<StoresListView />} />
        <Route path='/store/:id' element={<StoreView />} />
        <Route path='/counter' element={<Counter />} />
        <Route path='/login' element={<Login />} />
        <Route path='/logout' element={<Logout />} />
        <Route path='/orders' element={<OrdersListView />} />
        <Route path='/basket' element={<BasketView />} />
        <Route path='/makeOrder' element={<MakeOrderView />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;