using DapperWithTwoOrMoreDB.Core.Enums;
using DapperWithTwoOrMoreDB.Core.Extensions;
using DapperWithTwoOrMoreDB.Data.Interfaces;
using DapperWithTwoOrMoreDB.Data.Repositories;
using MySqlConnector;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace DapperWithTwoOrMoreDB.Data
{
    public class SessionFactory
    {
        private readonly ConfigAPI _configAPI;

        public SessionFactory(ConfigAPI configAPI)
        {
            _configAPI = configAPI;
        }

        public static IDbSession CreateSession(DatabaseType databaseType)
        {
            IDbConnection conn = CreateConnection(databaseType);
            IDbSession session = new DbSession(conn);
            return session;
        }

        private static IDbConnection CreateConnection(DatabaseType dataType)
        {
            IDbConnection conn;

            switch (dataType)
            {
                case DatabaseType.SqlServer:
                    conn = new SqlConnection(ConnConfig.ObterStringConexaoSqlServer());
                    break;
                case DatabaseType.Mysql:
                    conn = new MySqlConnection(ConnConfig.ObterStringConexaoMySql());
                    break;
                case DatabaseType.Oracle:
                    conn = new OracleConnection(ConnConfig.ObterStringConexaoOracle());
                    break;
                case DatabaseType.PostgreeSql:
                    conn = new NpgsqlConnection(ConnConfig.ObterStringConexaoPostgreeSql());
                    break;
                case DatabaseType.SqLite:
                    conn = new SQLiteConnection(ConnConfig.ObterStringConexaoSqLite());
                    break;
                default:
                    conn = new SqlConnection(ConnConfig.ObterStringConexaoSqlServer());
                    break;
            }

            conn.Open();

            return conn;
        }         
    }
}
