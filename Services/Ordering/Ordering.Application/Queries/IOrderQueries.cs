using Ordering.Application.Results;

namespace Ordering.Application.Queries
{
    public interface IOrderQueries
    {
        Task<OrderResult> GetOrder(Guid OrderId);
    }
}
