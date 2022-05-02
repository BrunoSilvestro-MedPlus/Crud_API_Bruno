using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Crud_API_Bruno.Domain.Products;
using Microsoft.AspNetCore.Http;

namespace Crud_API_Bruno.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/Categoria")]
    public class CategoriaController : ControllerBase
    {
        private ICategoriaService _CategoriaService;
        public CategoriaController(ICategoriaService CategoriaService)
        {
            _CategoriaService = CategoriaService;
        }

        /// <summary>
        /// Método responsável por buscar todas as Categorias
        /// </summary>        
        /// <returns></returns>
        /// <response code="200">Retorna uma lista de categorias</response>
        /// <response code="400">Retorna algum erro ao tentar obter uma lista de categorias</response>

        [HttpGet("Buscar Categorias")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult FindAll()
        {
            var result = _CategoriaService.FindAll();

            return Ok(result);
        }


        /// <summary>
        /// Método responsável por retornar uma categoia pelo id 
        /// </summary>
        /// <param name="CategoriaId">Identificador da categoria</param>    
        /// <returns></returns>
        /// <response code="200">Retorna uma categoia</response>
        /// <response code="400">Retorna algum erro ao tentar obter um categoia</response>
        /// <response code="404">Retorna categoia não encontrado</response>


        [HttpGet("Buscar Categoria by {id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult FindOne([FromRoute] int id)
        {
            var result = _CategoriaService.FindOne(id);

            return Ok(result);
        }

        /// <summary>
        /// Método responsável por atualizar os dados de uma categoria
        /// </summary>
        /// <param name="CategoriaId">Identificador da categoria</param>
        /// <returns>Dados da categoria</returns>
        /// <response code="200">Retorna os dados da categoria</response>
        /// <response code="404">Retorna categoria não encontrado</response>
        /// <response code="400">Retorna erro ao tentar retornar os dados da categoria</response>

        [HttpPut("Atualizar Categoria {id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Update([FromRoute] int id, [FromBody] Categoria body)
        {
            body.CategoriaId = id;

            var result = _CategoriaService.Update(body);

            if (!result.Completed)
            {
                return BadRequest(result);
            }

            return Ok(result.Result);
        }


        /// <summary>
        /// Método responsável por cadastrar uma categoria no banco de dados
        /// /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="201">Retorna os dados da categoria cadastrada</response>
        /// <response code="400">Retorna algum tipo de erro ao tentar cadastrar uma categoria</response>
        
        [HttpPost("Cadastrar Categoria")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult New([FromBody] Categoria body)
        {
            var result = _CategoriaService.Insert(body);

            if (!result.Completed)
            {
                return BadRequest(result);
            }

            return Ok(result.Result);
        }


        /// <summary>
        /// Método responsável por deletar uma categoria
        /// </summary>
        /// <param name="CategoriaId">Identificador da categoria</param>
        /// <returns></returns>
        /// <response code="200">categoria deletada com sucesso</response>
        /// <response code="400">Retorna algum erro ao tentar deletar a categoria</response>

        [HttpDelete("Deletar Categoria {id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _CategoriaService.Remove(id);

            if (!result.Completed)
            {
                return BadRequest(result);
            }

            return Ok();
        }
    }
}