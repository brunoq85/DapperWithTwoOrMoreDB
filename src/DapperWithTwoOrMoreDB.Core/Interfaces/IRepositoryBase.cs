using System.Collections.Generic;
using System.Data;

namespace DapperWithTwoOrMoreDB.Core.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        T GetById(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null, bool useTransaction = false);
        T GetBy(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null, bool useTransaction = false);
        IEnumerable<T> GetList(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null, bool useTransaction = false);
        int Execute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, bool useTransaction = true);
    }
}
