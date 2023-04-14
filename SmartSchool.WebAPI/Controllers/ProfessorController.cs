using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
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
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repository, IMapper mapper)
        {          
            _mapper = mapper;
            _repo = repository;
        }

        [HttpGet]
        public IActionResult Get(){
            var result = _repo.GetAllProfessores(true);
            var professorDto = _mapper.Map<IEnumerable<ProfessorDto>>(result);
            return Ok(professorDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            var professor = _repo.GetProfessorById(id, true);
            if(professor == null) return BadRequest("Professor não encontrado");

            var professorDto = _mapper.Map<ProfessorDto>(professor);
            return Ok(professorDto);
        }

        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repo.Add(professor);
            if(_repo.SaveChanges()) return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));

            return BadRequest("Professor não Cadastrado!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var prf = _repo.GetProfessorById(id, false);
            if(prf == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model, prf);

            _repo.Update(prf);
            if(_repo.SaveChanges()) return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(prf));

            return BadRequest("Professor não Atualizado!");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var prf = _repo.GetProfessorById(id, false);
            if(prf == null) return BadRequest("Professor não encontrado");

             _mapper.Map(model, prf);

            _repo.Update(prf);
            if(_repo.SaveChanges()) return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(prf));

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
