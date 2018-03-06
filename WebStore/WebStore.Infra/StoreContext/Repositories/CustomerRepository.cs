using System;
using System.Data;
using System.Linq;
using Dapper;
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
            return
            _dataAccessManager
                .Connection
                .Query<bool>(
                    "spCheckDocument",
                    new { Document = document },
                    commandType : CommandType.StoredProcedure)
                .FirstOrDefault();
        }

        public GetCustomerCommandResult GetById(Guid? id)
        {
            return
            _dataAccessManager
                .Connection
                .Query<GetCustomerCommandResult>(
                    "spGetCustomerById",
                    new { Id = id },
                    commandType : CommandType.StoredProcedure)
                .FirstOrDefault();
        }

        public GetCustomerCommandResult Get(string username)
        {
            throw new NotImplementedException();
        }

        public GetCustomerCommandResult GetByUsername(string username)
        {
            return
            _dataAccessManager
                .Connection
                .Query<GetCustomerCommandResult>(
                    "spGetCustomerByUsername",
                    new { Username = username },
                    commandType : CommandType.StoredProcedure)
                .FirstOrDefault();
        }

        public void Save(Customer customer)
        {
            _dataAccessManager.Connection.Execute("spCreateUser",
                new
                {
                    Id = customer.User.Id,
                        Username = customer.User.Username,
                        Password = customer.User.Password,
                        Active = customer.User.Active
                }, commandType : CommandType.StoredProcedure);

            _dataAccessManager.Connection.Execute("spCreateCustomer",
                new
                {
                    Id = customer.Id,
                    UserId = customer.User.Id,
                        FirstName = customer.Name.FirstName,
                        LastName = customer.Name.LastName,
                        Document = customer.Document.Number,
                        Email = customer.Email.Address
                }, commandType : CommandType.StoredProcedure);
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}