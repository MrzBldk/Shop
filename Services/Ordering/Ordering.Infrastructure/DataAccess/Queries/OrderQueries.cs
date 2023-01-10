using Microsoft.EntityFrameworkCore;
using Ordering.Application.Queries;
using Ordering.Application.Results;
using Ordering.Domain.Orders;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.DataAccess.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private readonly Context _context;
        public OrderQueries(Context context)
        {
            _context = context;
        }

        public async Task<List<OrderResult>> GetOrders()
        {
            List<Entities.Order> orders = await _context.Orders.ToListAsync();

            List<OrderResult> orderResults = new();

            foreach (var order in orders)
            {
                List<Entities.OrderItem> orderItems = await _context.OrderItems.Where(e => e.OrderId == order.Id).ToListAsync();
                
                ItemsCollection itemsCollection = new();
                foreach (var item in orderItems)
                    itemsCollection.Add(new OrderItem(item.OrderId, item.ProductId, item.UnitPrice, item.Name, item.Units));

                Order result = Order.Load(
                order.Id,
                new Address(
                    order.Street,
                    order.City,
                    order.State,
                    order.Country,
                    order.ZipCode
                    ),
                itemsCollection,
                order.OrderDate,
                order.OrderStatus,
                order.UserId
                );

                OrderResult orderResult = new(result);
                orderResults.Add(orderResult);
            }

            return orderResults;
        }

        public async Task<List<OrderResult>> GetOrdersByUser(Guid userId)
        {
            List<Entities.Order> orders = await _context.Orders.Where(e => e.UserId == userId).ToListAsync();

            List<OrderResult> orderResults = new();

            foreach (var order in orders)
            {
                List<Entities.OrderItem> orderItems = await _context.OrderItems.Where(e => e.OrderId == order.Id).ToListAsync();

                ItemsCollection itemsCollection = new();
                foreach (var item in orderItems)
                    itemsCollection.Add(new OrderItem(item.OrderId, item.ProductId, item.UnitPrice, item.Name, item.Units));

                Order result = Order.Load(
                order.Id,
                new Address(
                    order.Street,
                    order.City,
                    order.State,
                    order.Country,
                    order.ZipCode
                    ),
                itemsCollection,
                order.OrderDate,
                order.OrderStatus,
                order.UserId
                );

                OrderResult orderResult = new(result);
                orderResults.Add(orderResult);
            }

            return orderResults;
        }

        public async Task<OrderResult> GetOrder(Guid id)
        {
            Entities.Order order = await _context.Orders.FindAsync(id);
            if (order is null)
                throw new InfrastructureException($"Order with id {id} not found");

            List<Entities.OrderItem> orderItems = await _context.OrderItems.Where(e => e.OrderId == id).ToListAsync();

            ItemsCollection itemsCollection = new();
            foreach (var item in orderItems)
                itemsCollection.Add(new OrderItem(item.OrderId, item.ProductId, item.UnitPrice, item.Name, item.Units));

            Order result = Order.Load(
                order.Id,
                new Address(
                    order.Street,
                    order.City,
                    order.State,
                    order.Country,
                    order.ZipCode
                    ),
                itemsCollection,
                order.OrderDate,
                order.OrderStatus,
                order.UserId
                );

            OrderResult orderResult = new(result);

            return orderResult;
        }
    }
}
