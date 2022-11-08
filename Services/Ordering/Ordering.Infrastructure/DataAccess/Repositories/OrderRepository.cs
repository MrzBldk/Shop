﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Repositories;
using Ordering.Domain.Orders;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.DataAccess.Repositories
{
    public class OrderRepository : IOrderReadOnlyRepository, IOrderWriteOnlyRepository
    {
        private readonly Context _context;

        public OrderRepository(Context context)
        {
            _context = context;
        }

        public async Task<Order> Get(Guid id)
        {
            Entities.Order orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity is null)
                throw new InfrastructureException($"Order with id {id} not found");

            List<Entities.OrderItem> orderItemEntities = await _context.OrderItems.Where(e => e.Id == id).ToListAsync();

            ItemsCollection itemsCollection = new();
            foreach (var item in orderItemEntities)
                itemsCollection.Add(new OrderItem(item.OrderId, item.UnitPrice, item.Name, item.Units));

            Order order = Order.Load(
                orderEntity.Id,
                new Address(
                    orderEntity.Street,
                    orderEntity.City,
                    orderEntity.State,
                    orderEntity.Country,
                    orderEntity.ZipCode
                    ),
                itemsCollection,
                orderEntity.OrderDate
                );

            return order;
        }

        public async Task Add(Order order)
        {
            Entities.Order orderEntity = new()
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Street = order.Address.GetStreet(),
                City = order.Address.GetCity(),
                State = order.Address.GetState(),
                ZipCode = order.Address.GetZipCode(),
                Country = order.Address.GetCountry()
            };

            _context.Orders.Add(orderEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Order order, OrderItem item)
        {
            Entities.OrderItem orderItemEntity = new()
            {
                Id = item.Id,
                OrderId = order.Id,
                Name = item.Name,
                UnitPrice = item.UnitPrice,
                Units = item.Units
            };

            _context.OrderItems.Add(orderItemEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Order order)
        {
            string deleteSQL =
                    @"DELETE FROM OrderItems WHERE OrderId = {0};
                      DELETE FROM Orders WHERE Id = {0};";

            await _context.Database.ExecuteSqlRawAsync(deleteSQL, order.Id);
        }
    }
}