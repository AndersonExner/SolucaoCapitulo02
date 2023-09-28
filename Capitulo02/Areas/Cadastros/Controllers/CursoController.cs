using Capitulo02.Data;
using Capitulo02.Data.DAL.Cadastros;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;

namespace Capitulo02.Areas.Cadastros.Controllers
{
    [Area("Cadastros")]
    public class CursoController : Controller
    {
        private readonly IESContext _context;
        private readonly DepartamentoDAL departamentoDAL;
        private readonly CursoDAL cursoDAL;
    
        public CursoController(IESContext context)
        {
            _context = context;
            departamentoDAL = new DepartamentoDAL(context);
            cursoDAL = new CursoDAL(context);
        }

        public async Task<IActionResult> ObterVisaoCursoPorID(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await cursoDAL.ObterCursoPorId((long)id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        private bool CursoExists(long? id)
        {
            return _context.Cursos.Any(e => e.CursoID == id);
        }
        public async Task<IActionResult> Index()
        {
            return View(await cursoDAL.ObterCursosClassificadosPorNome().ToListAsync());
        }

        //CREATE
        public IActionResult Create()
        {
            var departamentos = departamentoDAL.ObterDepartamentos().ToList();
            departamentos.Insert(0, new Departamento() { DepartamentoID = 0, Nome = "Selecione o departamento" });
            ViewBag.Departamento = departamentos;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, DepartamentoID")]Curso curso)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await cursoDAL.GravarCurso(curso);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(curso);
        }

        //DETAIS

        public async Task<IActionResult> Details(long? id)
        {
            return await ObterVisaoCursoPorID(id);
        }

        //EDIT
        public async Task<IActionResult> Edit(long? id)
        {
            ViewResult visaoCurso = (ViewResult)await ObterVisaoCursoPorID(id);
            Curso curso = (Curso)visaoCurso.Model;
            ViewBag.Departamento = new SelectList(_context.Departamentos.OrderBy(i => i.Nome), "DepartamentoID", "Nome", curso.DepartamentoID);
            return visaoCurso;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("CursoID, Nome, DepartamentoID")]Curso curso)
        {
            if(id != curso.CursoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await cursoDAL.GravarCurso(curso);
                }catch (DbUpdateException)
                {
                    if (!CursoExists(curso.CursoID))
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

            ViewBag.Departamentos = new SelectList(_context.Departamentos.OrderBy(i => i.Nome), "DepartamentoID", "Nome", curso.DepartamentoID);
            return View(curso);
        }

        //DELETE
        public async Task<IActionResult> Delete (long? id)
        {
            return await ObterVisaoCursoPorID(id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var curso = await cursoDAL.RemoverCursoPorId((long)id);
            TempData["Message"] = "Departamento " + curso.Nome.ToUpper() + " foi removido";
            return RedirectToAction(nameof(Index));
        }

    }
}
