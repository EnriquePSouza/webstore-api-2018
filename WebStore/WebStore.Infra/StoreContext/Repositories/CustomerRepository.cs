using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.Queries;
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

        public bool CheckDocument(string document)
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

        public bool CheckEmail(string email)
        {
            return _dataAccessManager
                .Connection
                .Query<bool>(
                    "spCheckEmail",
                    new { Email = email },
                    commandType : CommandType.StoredProcedure)
                .FirstOrDefault();
        }

        public IEnumerable<ListCustomerQueryResult> Get()
        {
            return
                _dataAccessManager
                .Connection
                .Query<ListCustomerQueryResult>("SELECT [Id], CONCAT([FirstName], ' ', [LastName]) AS [Name], [Document], [Email] FROM [Customer]", new { });
        }

        public void Save(Customer customer)
        {
            _dataAccessManager.Connection.Execute("spCreateCustomer",
                new
                {
                    Id = customer.Id,
                        FirstName = customer.Name.FirstName,
                        LastName = customer.Name.LastName,
                        Document = customer.Document.Number,
                        Email = customer.Email.Address,
                        Phone = customer.Phone
                }, commandType : CommandType.StoredProcedure);

            foreach (var address in customer.Addresses)
            {
                _dataAccessManager.Connection.Execute("spCreateAddress",
                    new
                    {
                        Id = address.Id,
                            CustomerId = customer.Id,
                            Number = address.Number,
                            Complement = address.Complement,
                            District = address.District,
                            City = address.City,
                            State = address.State,
                            Country = address.Country,
                            ZipCode = address.ZipCode,
                            Type = address.Type,
                    }, commandType : CommandType.StoredProcedure);
            }
        }
    }
}