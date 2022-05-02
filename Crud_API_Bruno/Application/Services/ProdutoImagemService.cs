using Crud_API_Bruno.Application.ViewModels;
using Crud_API_Bruno.Domain.Products;
using Crud_API_Bruno.Domain.Produtos.ProdutoImagem;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_API_Bruno.Application.Services
{
    public class ProdutoImagemService : IProdutoImagemService
    {
        private readonly IProdutoImagemRepository _ProdutoImagemRepository;
        private readonly IProdutoRepository _produtoService;

        public ProdutoImagemService(
            IProdutoImagemRepository ProdutoImgagemRepository,
            IProdutoRepository produtoservice)
        {
            _ProdutoImagemRepository = ProdutoImgagemRepository;
            _produtoService = produtoservice;
        }

        public ActionResult Remove(int id)
        {

            try
            {
                _ProdutoImagemRepository.Delete(id);

                _ProdutoImagemRepository.Commit();

                return new ActionResult();
            }
            catch (Exception e)
            {
                return new ActionResult(e.Message);
            }
        }

        public IEnumerable<ProdutoImagem> FindAll()
        {
            return _ProdutoImagemRepository.Query();
        }

        public ProdutoImagem FindOne(int id)
        {
            return _ProdutoImagemRepository.FindById(id);
        }

        public ActionResult<ProdutoImagem> Insert(ProdutoImagem produtoImagem)
        {
           

            _ProdutoImagemRepository.Add(produtoImagem);

            _ProdutoImagemRepository.Commit();

            return new ActionResult<ProdutoImagem>(produtoImagem);

        }

        public ActionResult<ProdutoImagem> Update(ProdutoImagem produtoImagem)
        {

            if (!_ProdutoImagemRepository.Query().Any(x => x.ProdutoImagemId == produtoImagem.ProdutoImagemId))
            {
                throw new Exception("Registro não encontrado");
            }

            _ProdutoImagemRepository.Update(produtoImagem);

            _ProdutoImagemRepository.Commit();

            return new ActionResult<ProdutoImagem>(produtoImagem);

        }

       
        }
}

