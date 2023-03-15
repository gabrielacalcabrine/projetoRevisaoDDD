using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacaoRevisao.Domain.Contracts
{
    public class UsuarioRequest
    {
        [Required(ErrorMessage = "Nome do usuário é obrigatório.")]
        [StringLength(60, MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha do usuario é obrigatória.")]
        [StringLength(18, MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Data de nascimento é obrigatório.")]
        [DataType(DataType.DateTime)]
        public DateTime Nascimento { get; set; }
    }
}
