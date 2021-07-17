using Dapper;
using DapperWithTwoOrMoreDB.Core.Enums;
using DapperWithTwoOrMoreDB.Core.Interfaces;
using DapperWithTwoOrMoreDB.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DapperWithTwoOrMoreDB.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected virtual IDbSession DbSession
        {
            get { return SessionFactory.CreateSession(DataType); }
        }

        protected abstract DatabaseType DataType { get; }

        public virtual T GetById(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null, bool useTransaction = false)
        {
            if (string.IsNullOrEmpty(sql))
                return null;

            IDbSession session = DbSession;

            T result = session.Connection.Query<T>(sql, param, null, buffered, commandTimeout, commandType).SingleOrDefault();

            session.Dispose();

            return result;
        }
        public virtual T GetBy(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null, bool useTransaction = false)
        {
            if (string.IsNullOrEmpty(sql))
                return null;

            IDbSession session = DbSession;

            T result = session.Connection.Query<T>(sql, param, null, buffered, commandTimeout, commandType).FirstOrDefault();

            session.Dispose();

            return result;
        }
        public virtual IEnumerable<T> GetList(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null, bool useTransaction = false)
        {
            if (string.IsNullOrEmpty(sql))
                return null;

            IEnumerable<T> results;

            IDbSession session = DbSession;
            if (useTransaction)
            {
                session.BeginTrans();

                results = session.Connection.Query<T>(sql, param, session.Transaction, buffered, commandTimeout, commandType).ToList();
                session.Commit();
            }
            else
            {
                results = session.Connection.Query<T>(sql, param, null, buffered, commandTimeout, commandType).ToList();
            }

            session.Dispose(); // 释放资源

            return results;
        }
        public virtual int Execute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, bool useTransaction = true)
        {
            if (string.IsNullOrEmpty(sql))
                return 0;

            IDbSession session = DbSession;

            try
            {
                if (useTransaction)
                {
                    session.BeginTrans();

                    int rowsAffected = session.Connection.Execute(sql, param, session.Transaction, commandTimeout, commandType);
                    session.Commit();

                    return rowsAffected;
                }
                else
                {
                    return session.Connection.Execute(sql, param, null, commandTimeout, commandType);
                }
            }
            catch (Exception)
            {
                if (useTransaction)
                {
                    session.Rollback();
                }

                return 0;
            }
            finally
            {
                session.Dispose();
            }
        }
    }
}
