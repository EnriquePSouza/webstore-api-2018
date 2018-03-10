using System;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.QueryResults;
using WebStore.Domain.StoreContext.Repositories;

namespace WebStore.Tests.Mocks
{
    public class MockCustomerRepository : ICustomerRepository
    {
        private GetCustomerCommandResult _command;

        public bool DocumentExists(string document)
        {
            return document == "46718115533" ? false : true;
        }

        public GetCustomerCommandResult Get(string username)
        {
            throw new NotImplementedException();
        }

        public GetCustomerCommandResult GetById(Guid? id)
        {
            _command = new GetCustomerCommandResult();
            _command.Id = new Guid("74d96684-817d-4b5a-8edc-1a20aca2228c");
            _command.FirstName = "Enrique";
            _command.LastName = "Souza";
            _command.Document = "46718115533";
            _command.Email = "enrique@gmail.com";
            _command.UserId = new Guid("96352cd9-f793-42b1-bcb8-2f9c8698b330");
            _command.Username = "enrique";
            _command.Password = "1234567890";
            _command.Active = true;

            return _command;
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

        public bool UsernameExists(string username)
        {
            return username == "enrique" ? false : true;
        }
    }
}