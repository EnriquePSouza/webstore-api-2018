using System;
using System.Collections.Generic;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.QueryResults;

namespace WebStore.Domain.StoreContext.Repositories
{
    public interface ICustomerRepository
    {
        Customer GetById (Guid id);
        Customer GetByUsername(string username);
        GetCustomerCommandResult Get(string username);
        void Save(Customer customer);
        void Update(Customer customer);
        bool DocumentExists(string document);
    }
}