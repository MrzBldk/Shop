import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Counter } from './features/counter/Counter';
function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/counter' element={<Counter />}></Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;