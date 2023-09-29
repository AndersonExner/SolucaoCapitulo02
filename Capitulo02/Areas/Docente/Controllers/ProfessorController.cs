using Capitulo02.Data.DAL.Cadastros;
using Capitulo02.Data.DAL.Docente;
using Capitulo02.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelo.Docente;
using Modelo.Cadastros;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Capitulo02.Areas.Docente.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Cryptography;

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

        public void PrepararViewBags(List<Instituicao> instituicoes, List<Departamento> departamentos, List<Curso> cursos, List<Professor> professores)
        {
            instituicoes.Insert(0, new Instituicao() { InstituicaoID = 0, Nome = "Selecione a instituição" });
            ViewBag.Instituicoes = instituicoes;

            departamentos.Insert(0, new Departamento() { DepartamentoID = 0, Nome = "Selecione o departamento" });
            ViewBag.Departamentos = departamentos;

            cursos.Insert(0, new Curso() { CursoID = 0, Nome = "Selecione o curso" });
            ViewBag.Cursos = cursos;

            professores.Insert(0, new Professor() { ProfessorID = 0, Nome = "Selecione o professor" });
            ViewBag.Professores = professores;
        }

        [HttpGet]
        public IActionResult AdicionarProfessor()
        {
            PrepararViewBags(instituicaoDAL.ObterInstituicoesClassificadosPorNome().ToList(),
                        new List<Departamento>().ToList(), new List<Curso>().ToList(), new List<Professor>().ToList());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdicionarProfessor([Bind("InstituicaoID, DepartamentoID, CursoID, ProfessorID")] AdicionarProfessorViewModel model)
        {
            if (model.InstituicaoID == 0 || model.DepartamentoID == 0 || model.CursoID == 0 || model.ProfessorID == 0)
            {
                ModelState.AddModelError("", "É preciso selecionar todos os dados");
            }
            else
            {
                cursoDAL.RegistrarProfessor((long)model.CursoID, (long)model.ProfessorID);
                RegistrarProfessorNaSessao((long)model.CursoID, (long)model.ProfessorID);

                PrepararViewBags(instituicaoDAL.ObterInstituicoesClassificadosPorNome().ToList(),
                   departamentoDAL.ObterDepartamentosPorInstituicao((long)model.InstituicaoID).ToList(),
                   cursoDAL.ObterCursosPorDepartamento((long)model.DepartamentoID).ToList(),
                   cursoDAL.ObterProfessoresForaDoCurso((long)model.CursoID).ToList());
            }
            return View(model);
        }

        public void RegistrarProfessorNaSessao(long cursoID, long professorID)
        {
            var cursoProfessor = new CursoProfessor() { ProfessorID = professorID, CursoID = cursoID };
            List<CursoProfessor> cursosProfessor = new List<CursoProfessor>();
            string cursosProfessoresSession = HttpContext.Session.GetString("cursosProfessores");
            if (cursosProfessoresSession != null)
            {
                cursosProfessor = JsonConvert.DeserializeObject<List<CursoProfessor>>(cursosProfessoresSession);
            }
            cursosProfessor.Add(cursoProfessor);

            HttpContext.Session.SetString("cursosProfessores", JsonConvert.SerializeObject(cursosProfessor));
        }

        public IActionResult VerificarUltimosRegistros()
        {
            List<CursoProfessor> cursosProfessor = new List<CursoProfessor>();
            string cursosProfessoresSession = HttpContext.Session.GetString("cursosProfessores");
            if (cursosProfessoresSession != null)
            {
                cursosProfessor = JsonConvert.DeserializeObject<List<CursoProfessor>>(cursosProfessoresSession);
            }
            return View(cursosProfessor);
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

        public JsonResult ObterDepartamentosPorInstituicao(long actionID)
        {
            var departamentos = departamentoDAL.ObterDepartamentosPorInstituicao(actionID).ToList();
            return Json(new SelectList(departamentos, "DepartamentoID", "Nome"));
}
        public JsonResult ObterCursosPorDepartamento(long actionID)
        {
            var cursos = cursoDAL.ObterCursosPorDepartamento(actionID).ToList();
            return Json(new SelectList(cursos, "CursoID", "Nome"));
        }
        public JsonResult ObterProfessoresForaDoCurso(long actionID)
        {
            var professores = cursoDAL.ObterProfessoresForaDoCurso(actionID).ToList();
            return Json(new SelectList(professores, "ProfessorID", "Nome")
            );
        }

    }
}
