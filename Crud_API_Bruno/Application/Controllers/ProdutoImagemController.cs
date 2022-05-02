using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crud_API_Bruno.Domain.Products;
using Crud_API_Bruno.Domain.Produtos.ProdutoImagem;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Crud_API_Bruno.Infrastructure.Context;
using Crud_API_Bruno.Application.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Crud_API_Bruno.Application.Controllers
{
    [Route("api/ProdutoImagem")]
    [Authorize]
    [ApiController]
    public class ProdutoImagemController : ControllerBase
    {
        
        private readonly IHostingEnvironment _hostingEnv;
        private readonly MainContext _context;

        public ProdutoImagemController(MainContext context, IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
            _context = context;
        }


        /// <summary>
        /// Método responsável por cadastrar uma imagem de produto no banco de dados
        /// </summary>
        ///Exemplo
        ///{               
        ///  "produtoId": 0,
        ///   "CaminhoImagem=string 
        ///    }
        /// <returns></returns>
        /// <response code="201">Retorna a imagem cadastrada do produto</response>
        /// <response code="400">Retorna algum tipo de erro ao tentar inserir uma imagem</response>

        [HttpPost("Inserir Imagem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Microsoft.AspNetCore.Mvc.ActionResult> Upload([FromForm] ProdutoImagemVM produtoImagemVM)
        {
            if (produtoImagemVM.Image != null)
            {
                var a = _hostingEnv.WebRootPath;
                var fileName = Path.GetFileName(produtoImagemVM.Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Imagens", fileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await produtoImagemVM.Image.CopyToAsync(fileSteam)  ;
                }

                ProdutoImagem produtoImagem = new ProdutoImagem();
                produtoImagem.ProdutoId = produtoImagemVM.ProdutoId;
                produtoImagem.CaminhoImagem = filePath;  
                _context.Update(produtoImagem);
                await _context.SaveChangesAsync();
                return Ok(produtoImagem);
            }
            else
            {
                return BadRequest();
            }
        }



        /// <summary>
        /// Método responsável por retornar uma imagem pelo id de produto
        /// </summary>
        /// <param name="ProdutoId"></param>        
        /// <returns>imagem</returns>
        /// <response code="200">Retorna  imagem</response>
        /// <response code="400">Retorna algum erro ao tentar obter a imagem</response>
        /// <response code="404">Retorna imagem não encontrada</response>
        [HttpGet("Buscar Imagem by ProdutoId {id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Microsoft.AspNetCore.Mvc.ActionResult> Get(int id)
        {
            ProdutoImagem imagem = _context.ProdutoImagem.FirstOrDefault(m => m.ProdutoId == id);            
            if (System.IO.File.Exists(imagem.CaminhoImagem))
            {
                byte[] b = System.IO.File.ReadAllBytes(imagem.CaminhoImagem);
                return File(b, "image/jpg");
            }
            return null;
        }



        /// <summary>
        /// Método responsável por deletar uma imagem
        /// </summary>
        /// <param name="ProdutoId">Identificador do produto</param>
        /// <returns></returns>
        /// <response code="200">imagem deletada com sucesso</response>
        /// <response code="400">Retorna algum erro ao tentar deletar a imagem</response>

        [HttpDelete("Excluir Imagem by ProdutoId {id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Microsoft.AspNetCore.Mvc.ActionResult> Delete(int id)
        {
            ProdutoImagem imagem = _context.ProdutoImagem.FirstOrDefault(m => m.ProdutoId == id);
            if (System.IO.File.Exists(imagem.CaminhoImagem))
            {
                System.IO.File.Delete(imagem.CaminhoImagem);
                _context.Entry(imagem).State = EntityState.Deleted;
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }  
        }



        /// <summary>
        /// Método responsável por atualizar a foto de um produto
        /// </summary>
        /// <param name="ProdutoId">Identificador do produto</param>
        /// <returns>Dados da foto</returns>
        /// <response code="200">Retorna os dados da foto</response>
        /// <response code="404">Retorna foto não encontrada</response>
        /// <response code="400">Retorna erro ao tentar retornar os dados da foto</response>

        [HttpPut("Atualizar foto by produto id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Microsoft.AspNetCore.Mvc.ActionResult> Update([FromForm] ProdutoImagemVM produtoImagemVM)
        {
            ProdutoImagem imagem = _context.ProdutoImagem.FirstOrDefault(m => m.ProdutoId == produtoImagemVM.ProdutoId);
            if (System.IO.File.Exists(imagem.CaminhoImagem))
            {
                System.IO.File.Delete(imagem.CaminhoImagem);

                var a = _hostingEnv.WebRootPath;
                var fileName = Path.GetFileName(produtoImagemVM.Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Imagens", fileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await produtoImagemVM.Image.CopyToAsync(fileSteam);
                }


                imagem.ProdutoId = produtoImagemVM.ProdutoId;
                imagem.CaminhoImagem = filePath;
                _context.Update(imagem);
                _context.SaveChanges();
                return Ok(imagem);
            }                
            else
            {
                return BadRequest();
            }
        }

    }
}
