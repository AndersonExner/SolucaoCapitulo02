using Capitulo02.Models.Infra;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;
using Modelo.Discente;
using Modelo.Docente;

namespace Capitulo02.Data
{
    public class IESContext : IdentityDbContext<UsuarioDaAplicacao>
    {
        public IESContext(DbContextOptions<IESContext> options) : base(options)
        {
        }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Academico> Academicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CursoProfessor>()
                .HasKey(cd => new { cd.CursoID, cd.ProfessorID });

            modelBuilder.Entity<CursoProfessor>()
                .HasOne(c => c.Curso)
                .WithMany(cd => cd.CursosProfessores)
                .HasForeignKey(c => c.CursoID);

            modelBuilder.Entity<CursoProfessor>()
                .HasOne(d => d.Professor)
                .WithMany(cd => cd.CursosProfessores)
                .HasForeignKey(d => d.ProfessorID);
        }
    }
}