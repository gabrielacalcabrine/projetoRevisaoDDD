using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacaoRevisao.Domain.Contracts
{
    public class UsuarioResponse
    {
      
        public string Nome { get; set; }        
        public string Email { get; set; }        
        public string Cpf { get; set; }        
        public DateTime Nascimento { get; set; }
    }
}
