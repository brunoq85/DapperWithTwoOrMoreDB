using DapperWithTwoOrMoreDB.Core.Entities;
using DapperWithTwoOrMoreDB.Core.Interfaces;
using DapperWithTwoOrMoreDB.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperWithTwoOrMoreDB.Services.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository)
        {
            this.alunoRepository = alunoRepository;
        }

        public IEnumerable<Aluno> GetAllAlunos()
        {
            return this.alunoRepository.GetAllAlunos();
        }

        public Task<int> InsertAluno(Aluno aluno)
        {
            return this.alunoRepository.InsertAluno(aluno);
        }
    }
}
