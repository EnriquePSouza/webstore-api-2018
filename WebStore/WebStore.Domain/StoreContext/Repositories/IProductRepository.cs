using System;
using System.Collections.Generic;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.QueryResults;

namespace WebStore.Domain.StoreContext.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<GetProductListCommandResult> Get();
        GetProductListCommandResult GetById(Guid? id);
    }
}