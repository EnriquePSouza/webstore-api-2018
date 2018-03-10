using System;
using System.Collections.Generic;
using WebStore.Domain.StoreContext.QueryResults;
using WebStore.Domain.StoreContext.Repositories;

namespace WebStore.Tests.Mocks
{
    public class MockProductRepository : IProductRepository
    {
        private GetProductListCommandResult _command;

        public IEnumerable<GetProductListCommandResult> Get()
        {
            throw new NotImplementedException();
        }

        public GetProductListCommandResult GetById(Guid? id)
        {
            _command = new GetProductListCommandResult();
            _command.Id = new Guid("73319ab1-21a7-4fb7-9392-138ea772ef7a");
            _command.Title = "Mouse Gamer";
            _command.Image = "mouse.jpg";
            _command.Price = 100M;
            _command.QuantityOnHand = 10;

            return _command;
        }
    }
}