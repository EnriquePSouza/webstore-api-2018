using System;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.QueryResults;
using WebStore.Domain.StoreContext.Repositories;

namespace WebStore.Infra.StoreContext.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataAccessManager _dataAccessManager;

        public CustomerRepository(DataAccessManager dataAccessManager)
        {
            _dataAccessManager = dataAccessManager;
        }

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