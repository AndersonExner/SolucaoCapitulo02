﻿// <auto-generated />
using System;
using Capitulo02.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Capitulo02.Migrations
{
    [DbContext(typeof(IESContext))]
    [Migration("20230926200303_CursoDisciplina")]
    partial class CursoDisciplina
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Modelo.Cadastros.Curso", b =>
                {
                    b.Property<long?>("CursoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("CursoID"));

                    b.Property<long?>("DepartamentoID")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CursoID");

                    b.HasIndex("DepartamentoID");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("Modelo.Cadastros.CursoDisciplina", b =>
                {
                    b.Property<long?>("CursoID")
                        .HasColumnType("bigint");

                    b.Property<long?>("DisciplinaID")
                        .HasColumnType("bigint");

                    b.HasKey("CursoID", "DisciplinaID");

                    b.HasIndex("DisciplinaID");

                    b.ToTable("CursoDisciplina");
                });

            modelBuilder.Entity("Modelo.Cadastros.Departamento", b =>
                {
                    b.Property<long?>("DepartamentoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("DepartamentoID"));

                    b.Property<long?>("InstituicaoID")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartamentoID");

                    b.HasIndex("InstituicaoID");

                    b.ToTable("Departamentos");
                });

            modelBuilder.Entity("Modelo.Cadastros.Disciplina", b =>
                {
                    b.Property<long?>("DisciplinaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("DisciplinaID"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DisciplinaID");

                    b.ToTable("Disciplinas");
                });

            modelBuilder.Entity("Modelo.Cadastros.Instituicao", b =>
                {
                    b.Property<long?>("InstituicaoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("InstituicaoID"));

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstituicaoID");

                    b.ToTable("Instituicoes");
                });

            modelBuilder.Entity("Modelo.Cadastros.Curso", b =>
                {
                    b.HasOne("Modelo.Cadastros.Departamento", "Departamento")
                        .WithMany("Cursos")
                        .HasForeignKey("DepartamentoID");

                    b.Navigation("Departamento");
                });

            modelBuilder.Entity("Modelo.Cadastros.CursoDisciplina", b =>
                {
                    b.HasOne("Modelo.Cadastros.Curso", "Curso")
                        .WithMany("CursosDisciplinas")
                        .HasForeignKey("CursoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modelo.Cadastros.Disciplina", "Disciplina")
                        .WithMany("CursosDisciplinas")
                        .HasForeignKey("DisciplinaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Disciplina");
                });

            modelBuilder.Entity("Modelo.Cadastros.Departamento", b =>
                {
                    b.HasOne("Modelo.Cadastros.Instituicao", "Instituicao")
                        .WithMany("Departamentos")
                        .HasForeignKey("InstituicaoID");

                    b.Navigation("Instituicao");
                });

            modelBuilder.Entity("Modelo.Cadastros.Curso", b =>
                {
                    b.Navigation("CursosDisciplinas");
                });

            modelBuilder.Entity("Modelo.Cadastros.Departamento", b =>
                {
                    b.Navigation("Cursos");
                });

            modelBuilder.Entity("Modelo.Cadastros.Disciplina", b =>
                {
                    b.Navigation("CursosDisciplinas");
                });

            modelBuilder.Entity("Modelo.Cadastros.Instituicao", b =>
                {
                    b.Navigation("Departamentos");
                });
#pragma warning restore 612, 618
        }
    }
}
