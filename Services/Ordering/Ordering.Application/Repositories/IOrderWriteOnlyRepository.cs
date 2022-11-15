using Ordering.Domain.Orders;

namespace Ordering.Application.Repositories
{
    public interface IOrderWriteOnlyRepository
    {
        Task Add(Order order);
        Task Update(Order order);
        Task Update(Order order, OrderItem orderItem);
        Task Delete(Order order);
    }
}
