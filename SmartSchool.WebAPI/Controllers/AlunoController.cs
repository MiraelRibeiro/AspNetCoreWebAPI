using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SmartSchool.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;

        public AlunoController(IRepository repository)
        {         
            _repo = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllAlunos(true);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var aluno = _repo.GetAlunoById(id, true);
            if(aluno == null) return BadRequest("Aluno não encontrado");
            return Ok(aluno);
        }

        // POST api/<AlunoController>
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);
            if(_repo.SaveChanges()) return Ok(aluno);

            return BadRequest("Aluno não Cadastrado!");
        }

        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id, false);
            if(alu == null) return BadRequest("Aluno não encontrado");

            _repo.Update(aluno);
            if(_repo.SaveChanges()) return Ok(aluno);

            return BadRequest("Aluno não Atualizado!");
        }

         [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id, false);
            if(alu == null) return BadRequest("Aluno não encontrado");

            _repo.Update(aluno);
            if(_repo.SaveChanges()) return Ok(aluno);

            return BadRequest("Aluno não Atualizado!");
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if(aluno == null) return BadRequest("Aluno não encontrado");

            _repo.Delete(aluno);
            if(_repo.SaveChanges()) return Ok("Aluno deletado");

            return BadRequest("Aluno não Deletado!");
        }
    }
}
