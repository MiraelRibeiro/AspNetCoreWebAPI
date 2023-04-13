using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(includeProfessor){
                query = query.Include(a => a.AlunosDisciplinas)
                                    .ThenInclude(ad => ad.Disciplina)
                                    .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);
            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByIdDisciplina(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(includeProfessor){
                query = query.Include(a => a.AlunosDisciplinas)
                                    .ThenInclude(ad => ad.Disciplina)
                                    .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id).Where(al => al.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));
            return query.ToArray();
        }

        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(includeProfessor){
                query = query.Include(a => a.AlunosDisciplinas)
                                    .ThenInclude(ad => ad.Disciplina)
                                    .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id).Where(al => al.Id == alunoId);
            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessores(bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if(includeAlunos){
                query = query.Include(p => p.Disciplinas)
                                    .ThenInclude(ad => ad.AlunosDisciplinas)
                                    .ThenInclude(a => a.Aluno);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);
            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByIdDisciplina(int disciplinaId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if(includeAlunos){
                query = query.Include(p => p.Disciplinas)
                                    .ThenInclude(ad => ad.AlunosDisciplinas)
                                    .ThenInclude(a => a.Aluno);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id).Where(pf => pf.Disciplinas.Any(d => d.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId)));
            return query.ToArray();
        }

        public Professor GetProfessorById(int professorId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if(includeAlunos){
                query = query.Include(p => p.Disciplinas)
                                    .ThenInclude(ad => ad.AlunosDisciplinas)
                                    .ThenInclude(a => a.Aluno);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id).Where(pf => pf.Id == professorId);
            return query.FirstOrDefault();
        }

        
    }
}