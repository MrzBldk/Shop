import { CatalogView } from 'views/CatalogView';
import { ProductView } from 'views/ProductView';
import { StoreView } from 'views/StoreView';
import { Login } from 'components/auth/Login';
import { StoresListView } from 'views/StoresListView';
import { Logout } from 'components/auth/Logout';
import { OrdersListView } from 'views/OrdersListView';
import { BasketView } from 'views/BasketView';
import { MakeOrderView } from 'views/MakeOrderView';
import { AdminView } from 'views/AdminView';

const appRoutes = [
    {
        path: '/',
        requireAuth: false,
        element: <CatalogView />
    },
    {
        path: '/product/:id',
        requireAuth: false,
        element: <ProductView />
    },
    {
        path: '/stores',
        requireAuth: false,
        element: <StoresListView />
    },
    {
        path: '/store/:id',
        requireAuth: false,
        element: <StoreView />
    },
    {
        path: '/login',
        requireAuth: false,
        element: <Login />
    },
    {
        path: '/logout',
        requireAuth: false,
        element: <Logout />
    },
    {
        path: '/product/:id',
        requireAuth: false,
        element: <StoresListView />
    },
    {
        path: '/orders',
        requireAuth: true,
        element: <OrdersListView />
    },
    {
        path: '/basket',
        requireAuth: true,
        element: <BasketView />
    },
    {
        path: '/makeOrder',
        requireAuth: true,
        element: <MakeOrderView />
    },
    {
        path: '/admin',
        requireAuth: true,
        element: <AdminView />
    }
]

export default appRoutes