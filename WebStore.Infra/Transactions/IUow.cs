using System.Transactions;

namespace WebStore.Infra.Transactions
{
    public interface IUow
    {
        void Commit();
        void Rollback();
    }
}