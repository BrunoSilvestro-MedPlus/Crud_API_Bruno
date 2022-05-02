using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud_API_Bruno.Application.ViewModels;
using Crud_API_Bruno.Domain.Users;
using Crud_API_Bruno.Infrastructure.Context;

namespace Crud_API_Bruno.Application.Controllers
{
    [ApiController]
    [Route("api/v1/Autenticação")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private MainContext _mainContext;

        private IHttpContextAccessor _httpContext;

        public AuthController(IAuthService authService, MainContext mainContext, IHttpContextAccessor httpContext)
        {
            _authService = authService;
            _mainContext = mainContext;
            _httpContext = httpContext;
        }


        // <summary>
        /// Método responsável por pesquisar usuários do banco de dados
        /// </summary>       
        /// <returns></returns>
        /// <response code="200">Retorna uma lista de usuarios</response>
        /// <response code="400">Retorna algum erro ao tentar obter uma lista de usuarios</response>


        [HttpGet("Buscar Usuários")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            var userIdClaim = _httpContext.HttpContext.User.FindFirst(x => x.Type.Equals("Crud_UserId"));

            return Ok(_mainContext.Users.Find(Guid.Parse(userIdClaim.Value)));
        }

        /// <summary>
        /// Método responsável por cadastrar um usuário 
        /// </summary>
        /// <param name="E-mail">E-mail</param>
        /// <param name="Nome">Nome</param>
        /// <param name="Senha">Senha</param>
        /// <param name="Role">Tipo de acesso</param>
        /// <returns></returns>
        /// <response code="201">Retorna os dados do usuario cadastrado</response>
        /// <response code="400">Retorna algum tipo de erro ao tentar cadastrar um usuario</response>


        [HttpPost("Cadastrar Usuário")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] RegisterAction body)
        {
            var result = await _authService.Register(body);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        /// <summary>
        /// Método responsável por fazer login do usuário e retornar 
        /// </summary>
        /// <remarks>
        /// Login com email e password:
        ///
        ///     POST /api/v1/usuarios/login
        ///     {
        ///        "email": "teste@teste.com",
        ///        "password": "senhateste!",
        ///     }
        ///       
        /// Exemplo de resposta:
        ///
        ///     {
        ///         "accessToken": "JWT_TOKEN",
        ///         "tokenType": "Bearer",
        ///         "expiresAt": "2019-12-16T15:21:20.568078Z",
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="201">Retorna usuário logado com sucesso com o AccessToken e RefreshToken</response>
        /// <response code="400">Retorna erro ao tentar logar o usuário</response>


        [HttpPost("Acessar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Post([FromBody] SignInAction body)
        {
            var result = await _authService.Authenticate(body);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}