using System;
using System.Data.Entity;

namespace RssDataContext
{
    public class RssTransaction : IRssTransaction
    {
        private readonly DbContextTransaction _transation;

        public RssTransaction(Database database)
        {
            this._transation = database.BeginTransaction();
        }

        public void Dispose()
        {
            this._transation.Dispose();
        }

        public void Commit()
        {
            this._transation.Commit();
        }

        public void Rollback()
        {
            this._transation.Rollback();
        }
    }
}