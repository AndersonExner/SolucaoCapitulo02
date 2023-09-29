using Capitulo02.Data;
using Capitulo02.Data.DAL.Docente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capitulo02.Areas.Docente.Controllers
{
    [Area("Docente")]
    public class CursoProfessorController : Controller
    {

        private readonly IESContext _context;
        private readonly CursoProfessorDAL cursoProfessorDAL;

        public CursoProfessorController(IESContext context)
        {
            _context = context;
            cursoProfessorDAL = new CursoProfessorDAL(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await cursoProfessorDAL.ObterCursosProfessores().ToListAsync());
        }
    }
}
