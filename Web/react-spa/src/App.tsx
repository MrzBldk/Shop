import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Counter } from 'features/counter/Counter';
import { MainView } from 'views/MainView';
import { ProductView } from 'views/ProductView';
import { StoreView } from 'views/StoreView';
import { LoginView } from 'views/LoginView';
function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<MainView/>} />
        <Route path='/product/:id' element={<ProductView />} />
        <Route path='/store/:id' element={<StoreView />} />
        <Route path='/counter' element={<Counter />} />
        <Route path='/login' element={<LoginView />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;