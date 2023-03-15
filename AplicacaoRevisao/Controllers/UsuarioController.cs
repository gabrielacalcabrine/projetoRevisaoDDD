using AplicacaoRevisao.Domain.Contracts;
using AplicacaoRevisao.Domain.Interfaces;
using AplicacaoRevisao.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AplicacaoRevisao.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // podemos baixar biblioteca annotation para padronizar retornos do swagger com SwaggerResponse
        // para tratar erros, posso criar uma classe de filtros utilizando herança(ActionFilterAttribute) e chamar essa classe na minha controller. 
        //você pode usar o filtro criado como uma annotation acima dos métodos da controller. ex : [FiltroDeErroCustomizado]

        /// <summary>
        /// Realiza Login de usuário cadastrado.
        /// </summary>
        /// <returns>Usuário cadastrado</returns>
        /// <response code="201">Retorna usuário cadastrado</response>
        /// <response code="400">Se o item não for criado</response> 
        [HttpPost("login")]
        [ProducesResponseType(201)]
        //[Route("logar")]
        public async Task<ActionResult> Logar([FromBody]UsuarioLogin usuario)
        {
            return Ok(usuario);
        }

        /// <summary>
        /// Realiza cadastro de novo usuário.
        /// </summary> 
        /// <returns>Usuário cadastrado</returns>
        /// <response code="201">Retorna usuário cadastrado</response>
        /// <response code="400">Se o item não for criado</response> 
        [HttpPost]
        [ProducesResponseType(201)]
        //[Route("registrar")] 
        public async Task<ActionResult<UsuarioResponse>> Post([FromBody]UsuarioRequest usuario)
        {
            var result = await _usuarioService.Post(usuario);
            return Ok(result); // poderia ser created
        }

        /// <summary>
        /// Busca usuário e realiza mudança de dados.
        /// </summary>   
        ///<returns>Usuário modificado</returns>
        /// <response code="200">Se o objeto existe e foi alterado</response>
        /// <response code="404">Se o objeto não existe</response>
        /// <response code="403">Se o acesso for negado</response>
        [HttpPut]
        [ProducesResponseType(201)]
        //[Route("alterar")]
        public async Task<ActionResult<UsuarioResponse>> Put([FromBody] UsuarioRequest usuario, [FromRoute] int id)
        {
            var result = await _usuarioService.Put(usuario, id);
            return Ok(result);
        }

        /// <summary>
        /// Realiza busca de usuário por Id.
        /// </summary>
        /// <returns>Usuário</returns>
        /// <response code="200">Retorna usuário</response>
        /// <response code="404">Se o objeto não existe</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        //[Route("buscarPorId")]
        public async Task<ActionResult<UsuarioResponse>> GetById(int id)
        {
            var result = await _usuarioService.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// Realiza busca de todos os usuários.
        /// </summary>
        /// <returns>Todos os usuários cadastrados e ativos</returns>
        /// <response code="200">Retorna todos os usuários</response>
        /// <response code="403">Se o acesso for negado</response> 
        [HttpGet]
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [ProducesResponseType(200)]
        //[Route("listarTodos")]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> Get()
        {
            var result = await _usuarioService.Get();
            return Ok(result);
        }

        /// <summary>
        /// Busca usuário e realiza ativacao.
        /// </summary>   
        ///<returns></returns>
        /// <response code="200">Se o objeto existe e foi alterado</response>
        /// <response code="404">Se o objeto não existe</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [HttpPatch]
        [ProducesResponseType(200)]
        //[Route("ativar")]
        public async Task<ActionResult> PatchAtivar([FromBody] int id)
        {
            await _usuarioService.PatchAtivar(id);
            return Ok();
        }

        /// <summary>
        /// Busca usuário e desativa .
        /// </summary>   
        ///<returns></returns>
        /// <response code="200">Se o objeto existe e foi alterado</response>
        /// <response code="404">Se o objeto não existe</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilUsuarioAdmin)]
        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        //[Route("desativar")]
        public async Task<ActionResult> PatchDesativar([FromBody] int id)
        {
            await _usuarioService.PatchDesativar(id);
            return Ok();
        }

        /// <summary>
        /// Deleta usuário.
        /// </summary>            
        /// <response code="200">Se o objeto existe</response>
        /// <response code="404">Se o objeto não existe</response>
        /// <response code="403">Se o acesso for negado</response>
        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        //[Route("deletar")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {

            await _usuarioService.Delete(id);
            return NoContent();
        }

    }
}
