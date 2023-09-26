﻿using Modelo.Cadastros;
using Microsoft.EntityFrameworkCore;

namespace Capitulo02.Data
{
    public class IESContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public IESContext(DbContextOptions<IESContext> options) : base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Departamento>().ToTable("Departamento");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Instituicao>().ToTable("Instituicao");
        }
    }
}
