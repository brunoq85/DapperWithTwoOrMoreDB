using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperWithTwoOrMoreDB.Data
{
    public class ConnConfig
    {
        public static string ObterStringConexaoSqlServer()
        {
            var config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            return config.GetSection("connectionStrings")["SqlServerContext"];
        }

        public static string ObterStringConexaoOracle()
        {
            var config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            return config.GetSection("connectionStrings")["OracleContext"];
        }

        public static string ObterStringConexaoMySql()
        {
            var config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            return config.GetSection("connectionStrings")["MySqlContext"];
        }

        public static string ObterStringConexaoPostgreeSql()
        {
            var config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            return config.GetSection("connectionStrings")["PostgreeSqlContext"];
        }

        public static string ObterStringConexaoSqLite()
        {
            var config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            return config.GetSection("connectionStrings")["SqLiteContext"];
        }
    }
}
