﻿using Modelo.Cadastros;

namespace Capitulo02.Data
{
    public class IESDbInitializer
    {
        public static void Initialize(IESContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Departamentos.Any())
            {
                return;
            }

            var instituicoes = new Instituicao[]
            {
                new Instituicao { Nome = "UniParaná", Endereco = "Paraná" },
                new Instituicao { Nome = "UniAcre", Endereco = "Acre" },
                new Instituicao { Nome = "UniTeste", Endereco = "Roraima" }
            };

            foreach (Instituicao i in instituicoes)
            {
                context.Instituicoes.Add(i);
            }
            context.SaveChanges();

            var departamentos = new Departamento[]
            {
                new Departamento { Nome="Ciência da Computação", InstituicaoID = 1 },
                new Departamento { Nome="Ciência de Alimentos", InstituicaoID = 2 },
                new Departamento { Nome="Ciência de Dados", InstituicaoID = 3 }
            };

            foreach (Departamento d in departamentos)
            {
                context.Departamentos.Add(d);
            }
            context.SaveChanges();
        }
    }
}
