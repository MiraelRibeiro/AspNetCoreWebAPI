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
        private readonly DataContext _context;
        public ProfessorController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(){
            return Ok(_context.Professores);
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id){
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
            if(professor == null) return BadRequest("Professor não encontrado");
            return Ok(professor);
        }

        [HttpGet("byName")]
        public IActionResult GetByName(string nome){
            var professor = _context.Professores.FirstOrDefault(p => p.Nome == nome);
            if(professor == null) return BadRequest("Professor não encontrado");
            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prf = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if(prf == null) return BadRequest("Professor não encontrado");

            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

         [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prf = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if(prf == null) return BadRequest("Professor não encontrado");

            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
            if(professor == null) return BadRequest("Professor não encontrado");

            _context.Remove(professor);
            _context.SaveChanges();
            return Ok();
        }
    }
}
