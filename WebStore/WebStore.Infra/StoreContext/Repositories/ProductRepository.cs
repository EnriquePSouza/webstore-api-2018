using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.QueryResults;
using WebStore.Domain.StoreContext.Repositories;
using WebStore.Infra;

namespace WebStore.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataAccessManager _dataAccessManager;

        public ProductRepository(DataAccessManager dataAccessManager)
        {
            _dataAccessManager = dataAccessManager;
        }

        public IEnumerable<GetProductListCommandResult> Get()
        {
            return
            _dataAccessManager
                .Connection
                .Query<GetProductListCommandResult>(
                    "spListProducts", new { },
                    commandType : CommandType.StoredProcedure);
        }

        public Product GetById(Guid id)
        {
            return
            _dataAccessManager
                .Connection
                .Query<Product>(
                    "spGetProductById", new { Id = id },
                    commandType : CommandType.StoredProcedure)
                    .FirstOrDefault();
        }
    }
}