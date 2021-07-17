using DapperWithTwoOrMoreDB.Core.Entities;
using DapperWithTwoOrMoreDB.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DapperWithTwoOrMoreDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet("lista-alunos")]
        public IEnumerable<Aluno> Lista()
        {
            return _alunoService.GetAllAlunos();
        }
    }
}
