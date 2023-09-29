using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;

namespace Capitulo02.Data.DAL.Cadastros
{
    public class InstituicaoDAL
    {
        private IESContext _context;


        public InstituicaoDAL(IESContext context)
        {
            _context = context;
        }

        public IQueryable<Instituicao> ObterInstituicoesClassificadosPorNome()
        {
            return _context.Instituicoes.OrderBy(i => i.Nome);
        }

        public async Task<Instituicao> ObterInstituicaoPorID(long id)
        {
            return await _context.Instituicoes.Include(i => i.Departamentos).SingleOrDefaultAsync(d => d.InstituicaoID == id);
        }

        public async Task<Instituicao> GravarInstituicao(Instituicao instituicao)
        {
            if (instituicao.InstituicaoID == null)
            {
                _context.Instituicoes.Add(instituicao);
            }
            else
            {
                _context.Update(instituicao);
            }
            await _context.SaveChangesAsync();
            return instituicao;
        }

        public async Task<Instituicao> EliminarInstituicaoPorId(long id)
        {
            Instituicao instituicao = await ObterInstituicaoPorID(id);
            _context.Instituicoes.Remove(instituicao);
            await _context.SaveChangesAsync();
            return instituicao;
        }

        internal object ObterInstituicoesClassificadasPorNome()
        {
            throw new NotImplementedException();
        }
    }
}
