using System.Collections.Generic;
using WebStore.Domain.StoreContext.Entities;

namespace WebStore.Domain.StoreContext.Repositories
{
    public interface ICustomerRepository
    {
        bool CheckDocument(string document);
        bool CheckEmail(string email);
        void Save(Customer customer);
    }
}