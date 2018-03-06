using System;
using System.Collections.Generic;
using WebStore.Domain.StoreContext.QueryResults;
using WebStore.Domain.StoreContext.Repositories;

namespace WebStore.Tests.Mocks
{
    public class MockProductRepository : IProductRepository
    {
        public IEnumerable<GetProductListCommandResult> Get()
        {
            throw new NotImplementedException();
        }

        public GetProductListCommandResult GetById(Guid? id)
        {
            // The Method need a fake object to return fake data to orderHandler.
            throw new NotImplementedException();
        }
    }
}