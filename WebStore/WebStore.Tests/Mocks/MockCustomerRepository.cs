using System;
using System.Collections.Generic;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.Queries;
using WebStore.Domain.StoreContext.QueryResults;
using WebStore.Domain.StoreContext.Repositories;

namespace WebStore.Tests.Mocks
{
    public class MockCustomerRepository : ICustomerRepository
    {
        public bool DocumentExists(string document)
        {
            throw new NotImplementedException();
        }

        public Customer Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public GetCustomerCommandResult Get(string username)
        {
            throw new NotImplementedException();
        }

        public Customer GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void Save(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}