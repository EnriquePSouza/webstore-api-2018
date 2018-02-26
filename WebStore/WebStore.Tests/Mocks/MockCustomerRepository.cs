using System.Collections.Generic;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.Queries;
using WebStore.Domain.StoreContext.Repositories;

namespace WebStore.Tests.Mocks
{
    public class MockCustomerRepository : ICustomerRepository
    {
        public bool CheckDocument(string document)
        {
            return false;
        }

        public bool CheckEmail(string email)
        {
            return false;
        }

        public IEnumerable<ListCustomerQueryResult> Get()
        {
            throw new System.NotImplementedException();
        }

        public void Save(Customer customer)
        {
            
        }
    }
}