using AutoMapper;
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
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repository, IMapper mapper)
        {         
            _repo = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);
            
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var aluno = _repo.GetAlunoById(id, true);
            if(aluno == null) return BadRequest("Aluno não encontrado");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }

        // POST api/<AlunoController>
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
            if(_repo.SaveChanges()) return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não Cadastrado!");
        }

        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if(aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if(_repo.SaveChanges()) return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não Atualizado!");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if(aluno == null) return BadRequest("Aluno não encontrado");
            
            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if(_repo.SaveChanges()) return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

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
