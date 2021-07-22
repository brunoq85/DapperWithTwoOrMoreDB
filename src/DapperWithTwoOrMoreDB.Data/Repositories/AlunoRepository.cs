using Dapper;
using DapperWithTwoOrMoreDB.Core.Entities;
using DapperWithTwoOrMoreDB.Core.Enums;
using DapperWithTwoOrMoreDB.Core.Extensions;
using DapperWithTwoOrMoreDB.Core.Interfaces;
using DapperWithTwoOrMoreDB.Data.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DapperWithTwoOrMoreDB.Data.Repositories
{
    public class AlunoRepository : RepositoryBase<Aluno>, IAlunoRepository
    {
        private readonly ConfigAPI _configAPI;

        public AlunoRepository(IOptions<ConfigAPI> configAPI)
        {
            _configAPI = configAPI.Value;
        }

        protected override DatabaseType DataType
        {
            get
            {
                if (_configAPI.DatabaseStorage == "SQL Server")
                    return DatabaseType.SqlServer;
                else if(_configAPI.DatabaseStorage == "SqLite")
                    return DatabaseType.SqLite;
                else if (_configAPI.DatabaseStorage == "Oracle")
                    return DatabaseType.Oracle;

                return DatabaseType.SqlServer;
            }
        }

        public IEnumerable<Aluno> GetAllAlunos()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from Aluno");

            string sql = sb.ToString();
            IDbSession session = DbSession;
            try
            {
                using (IDbConnection conn = session.Connection)
                {
                    session.BeginTrans();

                    var alunos = conn.Query<Aluno>(sql, null, transaction: session.Transaction);

                    session.Commit();

                    return alunos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //session?.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        public async Task<int> InsertAluno(Aluno aluno)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Nome", aluno.Nome);
            queryParameters.Add("@Telefone", aluno.Telefone);
            queryParameters.Add("@Id", aluno.Id);

            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO Alunos VALUES (@Id, @Nome, @Telefone)");

            string sql = sb.ToString();

            IDbSession session = DbSession;
            try
            {
                using (IDbConnection conn = session.Connection)
                {
                    session.BeginTrans();

                    var alunos = conn.ExecuteAsync(sql, queryParameters, transaction: session.Transaction);

                    session.Commit();

                    return await alunos;
                }
            }
            catch (Exception)
            {
                return 1;
            }
            finally
            {
                session.Dispose();
            }



            return Execute(sql, new
            {
                aluno.Id,
                aluno.Nome,
                aluno.Telefone
            }, commandType: CommandType.Text);
        }
    }
}
