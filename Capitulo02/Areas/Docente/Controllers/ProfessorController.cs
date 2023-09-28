using Capitulo02.Data.DAL.Cadastros;
using Capitulo02.Data.DAL.Docente;
using Capitulo02.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelo.Docente;
using Modelo.Cadastros;

namespace Capitulo02.Areas.Docente.Controllers
{
    [Area("Docente")]
    public class ProfessorController : Controller
    {
        private readonly IESContext _context;
        private readonly InstituicaoDAL instituicaoDAL;
        private readonly DepartamentoDAL departamentoDAL;
        private readonly CursoDAL cursoDAL;
        private readonly ProfessorDAL professorDAL;
        
        public ProfessorController(IESContext context)
        {
            _context = context;
            instituicaoDAL = new InstituicaoDAL(context);
            departamentoDAL = new DepartamentoDAL(context);
            cursoDAL = new CursoDAL(context);
            professorDAL = new ProfessorDAL(context);
        }

        private async Task<bool> ProfessorExist(long? id)
        {
            return await professorDAL.ObterProfessorPorId((long)id) != null;
        }

        //OBTER VIEW POR IP
        private async Task<IActionResult> ObterVisaoProfessorPorId(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var professor = await professorDAL.ObterProfessorPorId((long)id);
            if(professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        public async Task<IActionResult> Index()
        {
            return View(await professorDAL.ObterProfessorPorNome().ToListAsync());
        }

        //CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome")] Professor professor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await professorDAL.GravarProfessor(professor);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir dados.");
            }
            return View(professor);
        }

        //EDIT
        public async Task<IActionResult> Edit(long? id)
        {
            return await ObterVisaoProfessorPorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("ProfessorID", "Nome")] Professor professor)
        {
            if(id != professor.ProfessorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await professorDAL.GravarProfessor(professor);
                }
                catch
                {
                    if(!await ProfessorExist(professor.ProfessorID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }

        //DETAIS
        public async Task<IActionResult> Details(long? id)
        {
            return await ObterVisaoProfessorPorId(id);
        }

        //DELETE

        public async Task<IActionResult> Delete(long? id)
        {
            return await ObterVisaoProfessorPorId(id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var inst = await professorDAL.RemoverProfessorPorId((long)id);
            TempData["Message"] = "Professor " + inst.Nome.ToUpper() + " foi removido.";
            return RedirectToAction(nameof(Index));
        }
    }
}
