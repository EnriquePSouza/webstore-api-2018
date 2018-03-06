using System;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.QueryResults;
using WebStore.Domain.StoreContext.Repositories;

namespace WebStore.Tests.Mocks
{
    public class MockCustomerRepository : ICustomerRepository
    {
        public bool DocumentExists(string document)
        {
            return false;
        }

        public GetCustomerCommandResult Get(string username)
        {
            throw new NotImplementedException();
        }

        public GetCustomerCommandResult GetById(Guid? id)
        {
            // The Method need a fake object to return fake data to orderHandler.
            throw new NotImplementedException();
        }

        public GetCustomerCommandResult GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void Save(Customer customer)
        {
            // Do Nothing.
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}