using Microsoft.EntityFrameworkCore;
using Modelo.Docente;

namespace Capitulo02.Data.DAL.Docente
{
    public class CursoProfessorDAL
    {
        private IESContext _context;

        public CursoProfessorDAL(IESContext context)
        {
            _context = context; 
        }

        public IQueryable<CursoProfessor> ObterCursosProfessores()
        {
            return _context.CursoProfessor.Include(i => i.Professor).Include(y => y.Curso);
        }
    }
}
