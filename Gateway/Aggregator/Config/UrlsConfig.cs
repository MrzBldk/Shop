namespace Aggregator.Config
{
    public class UrlsConfig
    {
        public string Ordering { get; set; }
        public string Basket { get; set; }
        public string Catalog { get; set; }

        public class BasketOperations
        {
            public static string GetById(string id) => $"api/Basket/{id}";
            public static string UpdateBasket() => "api/Basket";
        }

        public class OrderingOperations
        {
            public static string CreateOrder() => "api/Order";
            public static string AddMultiple(string id) => $"api/Order/{id}/addMultiple";
        }

        public class CatalogOperations
        {
            public static string GetItemById(string id) => $"api/Product/{id}";
        }
    }
}
