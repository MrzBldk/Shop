import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Counter } from 'features/counter/Counter';
import { MainView } from 'views/MainView';
import { ProductView } from 'views/ProductView';
import { StoreView } from 'views/StoreView';
function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<MainView/>} />
        <Route path='/product/:id' element={<ProductView />} />
        <Route path='/store/:id' element={<StoreView />} />
        <Route path='/counter' element={<Counter />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;