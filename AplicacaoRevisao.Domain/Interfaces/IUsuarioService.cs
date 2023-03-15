using AplicacaoRevisao.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacaoRevisao.Domain.Interfaces
{
    public interface IUsuarioService : IBaseService<UsuarioRequest, UsuarioResponse>
    {
        Task PatchAtivar(int id);
        Task PatchDesativar(int id);
    }
}
