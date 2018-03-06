using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.Repositories;

namespace WebStore.Tests.Mocks
{
    public class MockOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            // Do Nothing.
        }
    }
}