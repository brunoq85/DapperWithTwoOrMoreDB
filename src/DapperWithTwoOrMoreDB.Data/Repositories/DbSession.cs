using DapperWithTwoOrMoreDB.Data.Interfaces;
using System;
using System.Data;

namespace DapperWithTwoOrMoreDB.Data.Repositories
{
    public class DbSession : IDbSession
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public DbSession(IDbConnection conn)
        {
            _connection = conn;
        }

        public IDbConnection Connection
        {
            get { return _connection; }
        }

        public IDbTransaction Transaction
        {
            get { return _transaction; }
        }

        public IDbTransaction BeginTrans(IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            _transaction = _connection.BeginTransaction(isolation);

            return _transaction;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            try
            {
                if (_transaction != null)
                {
                    _transaction?.Dispose();
                    _transaction = null;
                }

                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();

                    _connection?.Dispose();
                    _connection = null;
                }

                GC.SuppressFinalize(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
