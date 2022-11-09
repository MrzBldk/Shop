using Ordering.Domain.Orders;

namespace Ordering.Application.Repositories
{
    public interface IOrderReadOnlyRepository
    {
        Task<Order> Get(Guid id);
    }
}
