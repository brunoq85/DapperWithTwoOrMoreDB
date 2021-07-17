using DapperWithTwoOrMoreDB.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperWithTwoOrMoreDB.Services.Interface
{
    public interface IAlunoService
    {
        IEnumerable<Aluno> GetAllAlunos();
        Task<int> InsertAluno(Aluno aluno);
    }
}
