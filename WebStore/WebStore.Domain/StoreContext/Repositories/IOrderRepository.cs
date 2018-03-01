using WebStore.Domain.StoreContext.Entities;

namespace WebStore.Domain.StoreContext.Repositories
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }
}