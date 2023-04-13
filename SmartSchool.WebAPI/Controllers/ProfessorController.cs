using Microsoft.AspNetCore.Http;
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
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        public ProfessorController(IRepository repository)
        {          
            _repo = repository;
        }

        [HttpGet]
        public IActionResult Get(){
            var result = _repo.GetAllProfessores(true);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            var professor = _repo.GetProfessorById(id, true);
            if(professor == null) return BadRequest("Professor não encontrado");
            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);
            if(_repo.SaveChanges()) return Ok(professor);

            return BadRequest("Professor não Cadastrado!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prf = _repo.GetProfessorById(id, false);
            if(prf == null) return BadRequest("Professor não encontrado");

            _repo.Update(professor);
            if(_repo.SaveChanges()) return Ok(professor);

            return BadRequest("Professor não Atualizado!");
        }

         [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prf = _repo.GetProfessorById(id, false);
            if(prf == null) return BadRequest("Professor não encontrado");

            _repo.Update(professor);
            if(_repo.SaveChanges()) return Ok(professor);

            return BadRequest("Professor não Atualizado!");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
            if(professor == null) return BadRequest("Professor não encontrado");

            _repo.Delete(professor);
            if(_repo.SaveChanges()) return Ok("Professor deletado");

            return BadRequest("Professor não Deletado!");
        }
    }
}
