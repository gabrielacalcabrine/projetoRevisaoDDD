using AplicacaoRevisao.Domain.Entities;
using AplicacaoRevisao.Domain.Interfaces;
using AplicacaoRevisao.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacaoRevisao.Repository.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AplicacaoRevisaoContext context) : base(context)
        {
        }
    }
}
