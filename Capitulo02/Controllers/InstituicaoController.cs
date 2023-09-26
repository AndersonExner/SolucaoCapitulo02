using Capitulo02.Data;
using Capitulo02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Capitulo02.Controllers
{
    public class InstituicaoController : Controller
    {
        private readonly IESContext _context;

        public InstituicaoController(IESContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Instituicoes.OrderBy(c => c.Nome).ToListAsync());
        }

        //CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome", "Endereco")] Instituicao instuicao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(instuicao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir dados.");
            }
            return View(instuicao);
        }

        //EDIT
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inst = await _context.Instituicoes.SingleOrDefaultAsync(m => m.InstituicaoID == id);

            if (inst == null)
            {
                return NotFound();
            }
            return View(inst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("InstituicaoID","Nome", "Endereco")] Instituicao instituicao)
        {
            if (id != instituicao.InstituicaoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instituicao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstituicaoExists(instituicao.InstituicaoID))
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
            return View(instituicao);
        }

        private bool InstituicaoExists(long? id)
        {
            return _context.Instituicoes.Any(e => e.InstituicaoID == id);
        }

        //DETAIS
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituicao = await _context.Instituicoes.Include(d => d.Departamentos).SingleOrDefaultAsync(m => m.InstituicaoID == id);
            if (instituicao == null)
            {
                return NotFound();
            }

            return View(instituicao);
        }

        //DELETE
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inst = await _context.Instituicoes.SingleOrDefaultAsync(i => i.InstituicaoID == id);
            if (inst == null)
            {
                return NotFound();
            }

            return View(inst);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var inst = await _context.Instituicoes.SingleOrDefaultAsync(i => i.InstituicaoID == id);
            _context.Instituicoes.Remove(inst);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Instituição " + inst.Nome.ToUpper() + " foi removida.";
            return RedirectToAction(nameof(Index));
        }
        
    }
}
