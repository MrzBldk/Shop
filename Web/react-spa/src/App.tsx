import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Navbar } from 'components/Navbar';
import appRoutes from 'appRoutes';
import { AuthorizeRoute } from 'components/auth/AuthorizeRoute';
function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <Routes>
        {appRoutes.map((route, index) => {
          const { element, path, requireAuth } = route
          return <Route key={index} path={path}
            element={requireAuth ?
              <AuthorizeRoute element={element} path={path.substring(1)} /> :
              element
            }
          />
        })}
      </Routes>
    </BrowserRouter>
  );
}

export default App;