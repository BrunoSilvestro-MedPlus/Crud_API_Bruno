using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Crud_API_Bruno.Domain.Products;

using System.IO;
using Microsoft.AspNetCore.Http;

namespace Crud_API_Bruno.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/produtos")]
    public class ProdutoController : ControllerBase
    {
        private IProdutoService _produtoService;
        public ProdutoController(IProdutoService ProdutoService)
        {
            _produtoService = ProdutoService;
        }


        /// <summary>
        /// Método responsável por buscar todos os produtos
        /// </summary>        
        /// <returns></returns>
        /// <response code="200">Retorna uma lista de produtos</response>
        /// <response code="400">Retorna algum erro ao tentar obter uma lista de produtos</response>

        [HttpGet("Buscar Produtos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult FindAll()
        {
            var result = _produtoService.FindAll();

            return Ok(result);
        }


        /// <summary>
        /// Método responsável por buscar um produto pelo id 
        /// </summary>
        /// <param name="ProdutoId">Identificador do produto</param>    
        /// <returns></returns>
        /// <response code="200">Retorna uma produto</response>
        /// <response code="400">Retorna algum erro ao tentar obter um produto</response>
        /// <response code="404">Retorna produtonão encontrado</response>

        [HttpGet("Buscar Produto by {id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        public IActionResult FindOne([FromRoute] int id)
        {
            var result = _produtoService.FindOne(id);

            return Ok(result);
        }


        /// <summary>
        /// Método responsável por atualizar os dados de um produto
        /// </summary>
         /// <param name="ProdutoId">Identificador do produto</param>
        /// <returns>Dados do produto</returns>
        /// <response code="200">Retorna os dados do produto</response>
        /// <response code="404">Retorna produto não encontrado</response>
        /// <response code="400">Retorna erro ao tentar retornar os dados do produto</response>

        [HttpPut("Atualizar Produto by {id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Update([FromRoute] int id, [FromBody] Produto body)
        {
            body.ProdutoId = id;

            var result = _produtoService.Update(body);

            if (!result.Completed)
            {
                return BadRequest(result);
            }

            return Ok(result.Result);
        }

        /// <summary>
        /// Método responsável por cadastrar um produto no banco de dados
        /// </summary>
        /// Exemplo
        /// {
        ///  "produtoId": 0,
        ///  "nome": "string",
        ///  "descricao": "string",
        ///  "preco": 0,
        ///  "codigoBarra": 0,
        ///  "data": "2022-05-01T19:56:54.868Z",
        ///  "altura": 0,
        ///  "largura": 0,
        ///  "comprimento": 0,
        ///  "produtoImagems": {
        ///    "produtoImagemId": 0,
        ///    "produtoId": 0,
        ///    "caminhoImagem": "string"
        ///  },
        ///  "produtosCategorias": [
        ///    {
        ///      "produtosCategoriaId": 0,
        ///      "categoriaId": 0,
        ///      "produtoId": 0,
        ///      "produto": "string"
        ///    }
        ///  ]
        ///}
        /// <returns></returns>
        /// <response code="201">Retorna os dados da produto cadastrada</response>
        /// <response code="400">Retorna algum tipo de erro ao tentar cadastrar um produto</response>
        
        [HttpPost("Cadastrar Produto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult New([FromBody] Produto body)
        {
            var result = _produtoService.Insert(body);

            if (!result.Completed)
            {
                return BadRequest(result);
            }

            return Ok(result.Result);
        }

        /// <summary>
        /// Método responsável por deletar uma categoria
        /// </summary>
        /// <param name="ProdutoId">Identificador do produto</param>
        /// <returns></returns>
        /// <response code="200">categoria deletada com sucesso</response>
        /// <response code="400">Retorna algum erro ao tentar deletar a categoria</response>

        [HttpDelete("Deletar Produto {id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _produtoService.Remove(id);

            if (!result.Completed)
            {
                return BadRequest(result);
            }

            return Ok();
        }

      
    }
}
