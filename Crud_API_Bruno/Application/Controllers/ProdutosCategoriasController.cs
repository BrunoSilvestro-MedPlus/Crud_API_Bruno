using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Crud_API_Bruno.Domain.Products;
using Microsoft.AspNetCore.Http;

namespace Crud_API_Bruno.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/ProdutosCategorias")]
    public class ProdutosCategoriasController : ControllerBase
    {
        private IProdutosCategoriasService _produtoscategoriasService;
        public ProdutosCategoriasController(IProdutosCategoriasService produtoscategoriasService)
        {
            _produtoscategoriasService = produtoscategoriasService;
        }


        /// <summary>
        /// Método responsável por buscar todas os produtoscategorias
        /// </summary>        
        /// <returns></returns>
        /// <response code="200">Retorna uma lista de produtoscategorias</response>
        /// <response code="400">Retorna algum erro ao tentar obter uma lista de produtoscategorias</response>

        [HttpGet("Buscar ProdutoCategoria")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult FindAll()
        {
            var result = _produtoscategoriasService.FindAll();

            return Ok(result);
        }

        /// <summary>
        /// Método responsável por retornar um produtoscategorias pelo id 
        /// </summary>
        /// <param name="ProdutoCategoriaId"></param>        
        /// <returns>Dados do produtoscategorias</returns>
        /// <response code="200">Retorna um produtoscategorias</response>
        /// <response code="400">Retorna algum erro ao tentar obter um produtoscategorias</response>
        /// <response code="404">Retorna produtoscategorias não encontrado</response>


        [HttpGet("Buscar ProdutoCategoria by {id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        public IActionResult FindOne([FromRoute] int id)
        {
            var result = _produtoscategoriasService.FindOne(id);

            return Ok(result);
        }

        /// <summary>
        /// Método responsável por atualizar os dados de um produtoscategorias
        /// </summary>
        /// <returns>Dados do produtoscategorias</returns>
        /// <response code="200">Retorna os dados do produtoscategorias</response>
        /// <response code="404">Retorna produtoscategorias não encontrado</response>
        /// <response code="400">Retorna erro ao tentar retornar os dados do produtoscategorias</response>

        [HttpPut("Atualizar ProdutoCategoria {id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        
        public IActionResult Update([FromRoute] int id, [FromBody] ProdutosCategorias body)
        {
            body.CategoriaId = id;

            var result = _produtoscategoriasService.Update(body);

            if (!result.Completed)
            {
                return BadRequest(result);
            }

            return Ok(result.Result);
        }


        /// <summary>
        /// Método responsável por cadastrar um produtoscategorias no banco de dados
        /// </summary>
        ///Exemplo
        ///{
        ///  "produtosCategoriaId": 0,
        ///  "categoriaId": 0,
        ///  "produtoId": 0,
        ///  "produto": {
        ///    "produtoId": 0,
        ///    "nome": "string",
        ///    "descricao": "string",
        ///    "preco": 0,
        ///    "codigoBarra": 0,
        ///    "data": "2022-05-01T20:07:39.930Z",
        ///    "altura": 0,
        ///    "largura": 0,
        ///    "comprimento": 0
        ///    }
        /// <returns></returns>
        /// <response code="201">Retorna os dados do produtoscategorias cadastrada</response>
        /// <response code="400">Retorna algum tipo de erro ao tentar cadastrar um produtoscategorias</response>

        [HttpPost("Cadastrar ProdutoCategoria")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public IActionResult New([FromBody] ProdutosCategorias body)
        {
            var result = _produtoscategoriasService.Insert(body);

            if (!result.Completed)
            {
                return BadRequest(result);
            }

            return Ok(result.Result);
        }

        /// <summary>
        /// Método responsável por deletar uma produtoscategorias
        /// </summary>
        /// <param name="ProdutoCategoriaId">Identificador da produtoscategorias</param>
        /// <returns></returns>
        /// <response code="200">produtoscategorias deletado com sucesso</response>
        /// <response code="400">Retorna algum erro ao tentar deletar o produtoscategorias</response>

        [HttpDelete("Deletar ProdutoCategoria {id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _produtoscategoriasService.Remove(id);

            if (!result.Completed)
            {
                return BadRequest(result);
            }

            return Ok();
        }
    }
}