using Ordering.Application.Results;

namespace Ordering.Application.Queries
{
    public interface IOrderQueries
    {
        Task<List<OrderResult>> GetOrders();
        Task<List<OrderResult>> GetOrdersByUser(Guid userId)
        Task<OrderResult> GetOrder(Guid OrderId);
    }
}
