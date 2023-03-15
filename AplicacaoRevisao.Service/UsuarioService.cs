using AplicacaoRevisao.Domain.Contracts;
using AplicacaoRevisao.Domain.Entities;
using AplicacaoRevisao.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacaoRevisao.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task PatchAtivar(int id)
        {
            var usuario = await _usuarioRepository.FindAsync(id);
            if(usuario == null)
            {
                throw new Exception("Não existe usuário com esse ID. ");
            }
            if(usuario.Ativo == true)
            {
                throw new Exception("Usuário já está ativo ");
            }
           
            usuario.Ativo = true;                       

        }

        public async Task PatchDesativar(int id)
        {
            var usuario = await _usuarioRepository.FindAsync(id);
            if (usuario == null)
            {
                throw new Exception("Não existe usuário com esse ID. ");
            }
            if (usuario.Ativo == false)
            {
                throw new Exception("Usuário já está Inativo ");
            }

            usuario.Ativo = false;

        }

        public async Task Delete(int request)
        {
            var usuario = await _usuarioRepository.FindAsync(request);
            if(usuario == null )
            {
                throw new Exception("Não existe usuário com esse ID. ");
            }
            if(usuario.Ativo == false)
            {
                throw new Exception("Usuário já está inativo. ");                   
            }
            usuario.Ativo = false;
            await _usuarioRepository.EditAsync(usuario);
        }

        public async Task<IEnumerable<UsuarioResponse>> Get()
        {            
            var lista = await _usuarioRepository.ListAsync(x => x.Ativo);
            return _mapper.Map<IEnumerable<UsuarioResponse>>(lista);
        }

        public async Task<UsuarioResponse> GetById(int id)
        {
            var buscaUsuario = await _usuarioRepository.FindAsync(id);
            return _mapper.Map<UsuarioResponse>(buscaUsuario);
        }

        public async Task<UsuarioResponse> Post(UsuarioRequest request)
        {
            // verificar cpf
            // verificar se email ja existe em banco pois não pode ser repetido
            // verificar se cpf já existe em banco
            if (ValidacaoCpf(request.Cpf) == false)
            {
                throw new ArgumentException("Cpf Inválido");
            }
            var usuario = _mapper.Map<Usuario>(request);
            var usuarioCadastrado = await _usuarioRepository.AddAsync(usuario);
            return _mapper.Map<UsuarioResponse>(usuarioCadastrado);
        }

        public async Task<UsuarioResponse> Put(UsuarioRequest request, int? id)
        {
            var usuario = await _usuarioRepository.FindAsync((int)id);
            if (usuario == null)
            {
                throw new ArgumentException("usuário não pode ser modificado pois não existe ");
            }
            if(usuario.Ativo == false)
            {
                throw new ArgumentException("usuário está inativo. Ative - o antes de modificar ");
            }
            var usuarioModificado = await _usuarioRepository.EditAsync(usuario);
            return _mapper.Map<UsuarioResponse>(usuarioModificado);
        }

        public bool ValidacaoCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;

            string digito;

            int soma;

            int resto;

            cpf = cpf.Trim();

            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
            {

                return false;
            }
            var repetidos = int.Parse(string.Concat(cpf.GroupBy(x => x).Select(x => x.Count())));
            if (repetidos == 11)
            {
                return false;
            }

            tempCpf = cpf.Substring(0, 9);

            soma = 0;

            for (int i = 0; i < 9; i++)

                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);

        }

    }
}
