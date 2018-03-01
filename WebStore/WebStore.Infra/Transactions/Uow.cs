using System.Transactions;
using WebStore.Infra;

namespace WebStore.Infra.Transactions
{
    public class Uow : IUow
    {
        private readonly DataAccessManager _dataAccessmanager;

        public Uow(DataAccessManager dataAccessmanager)
        {
            _dataAccessmanager = dataAccessmanager;
        }

        public void Commit()
        {
            _dataAccessmanager.Connection.BeginTransaction().Commit();
        }

        public void Rollback()
        {
            _dataAccessmanager.Connection.BeginTransaction().Rollback();
        }
    }
}