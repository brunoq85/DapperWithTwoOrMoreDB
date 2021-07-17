using DapperWithTwoOrMoreDB.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperWithTwoOrMoreDB.Core.Interfaces
{
    public interface IAlunoRepository : IRepositoryBase<Aluno>
    {
        IEnumerable<Aluno> GetAllAlunos();

        Task<int> InsertAluno(Aluno aluno);
    }
}
