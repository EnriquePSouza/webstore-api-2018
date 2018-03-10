using System;
using System.Data;
using Dapper;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.Repositories;
using WebStore.Infra;

namespace WebStore.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataAccessManager _dataAccessManager;

        public OrderRepository(DataAccessManager dataAccessManager)
        {
            _dataAccessManager = dataAccessManager;
        }

        public void Save(Order order)
        {
            _dataAccessManager.Connection.Execute("spCreateOrder",
            new
            {
                Id = order.Id,
                CustomerId = order.Customer.Id,
                CreateDate = DateTime.Now,
                Status = order.Status
            }, commandType: CommandType.StoredProcedure);

            foreach (var item in order.Items)
            {
                _dataAccessManager.Connection.Execute("spCreateOrderItem",
                new
                {
                   Id = item.Id,
                   OrderId = order.Id,
                   ProductId = item.Product.Id,
                   Quantity = item.Quantity,
                   Price = item.Price 
                }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}