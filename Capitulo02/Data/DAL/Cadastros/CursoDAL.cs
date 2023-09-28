using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;

namespace Capitulo02.Data.DAL.Cadastros
{
    public class CursoDAL
    {
        private IESContext _context;
        
        public CursoDAL(IESContext context)
        {
            _context = context;
        }

        public IQueryable<Curso> ObterCursosClassificadosPorNome()
        {
            return _context.Cursos.Include(i => i.Departamento).OrderBy(y => y.Nome);
        } 

        public async Task<Curso> ObterCursoPorId(long id)
        {
            var curso = await _context.Cursos.SingleOrDefaultAsync(i => i.CursoID == id);
            _context.Departamentos.Where(i => curso.DepartamentoID == i.DepartamentoID).Load();
            return (curso);
        }

        public async Task<Curso> GravarCurso(Curso curso)
        {
            if(curso.CursoID == null)
            {
                _context.Cursos.Add(curso);
            }
            else
            {
                _context.Update(curso);
            }
            await _context.SaveChangesAsync();
            return curso;
        }

        public async Task<Curso> RemoverCursoPorId(long id)
        {
            Curso curso = await ObterCursoPorId(id);
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            
            return curso;   
        }
    }
}
