using System.Threading;
using System;
using System.Collections.Generic;
using Crud_API_Bruno.Application.ViewModels;
using System.Linq;
using Crud_API_Bruno.Domain.Products;
using FluentValidation.Results;

namespace Crud_API_Bruno.Application.Services
{
    public class ProdutosCategoriasService : IProdutosCategoriasService
    {
        private readonly IProdutosCategoriasRepository _ProdutoRepository;
        private readonly ICategoriaRepository _categoriaService;

        public ProdutosCategoriasService(
            IProdutosCategoriasRepository ProdutoRepository,
            ICategoriaRepository categoriaervice)
        {
            _ProdutoRepository = ProdutoRepository;
            _categoriaService = categoriaervice;
        }

        public ActionResult Remove(int id)
        {
          
            try
            {
                _ProdutoRepository.Delete(id);

                _ProdutoRepository.Commit();

                return new ActionResult();
            }
            catch (Exception e)
            {
                return new ActionResult(e.Message);
            }
        }

        public IEnumerable<ProdutosCategorias> FindAll()
        {
            return _ProdutoRepository.Query();
        }

        public ProdutosCategorias FindOne(int id)
        {
            return _ProdutoRepository.FindById(id);
        }

        public ActionResult<ProdutosCategorias> Insert(ProdutosCategorias produtosCategorias)
        {
            ProdutosCategoriasValidator validator = new ProdutosCategoriasValidator();
            ValidationResult results = validator.Validate(produtosCategorias);

            if (!results.IsValid)
            {
                string allMessages = results.ToString(" -- ");
                return new ActionResult<ProdutosCategorias>(allMessages);
            }

            _ProdutoRepository.Add(produtosCategorias);

            _ProdutoRepository.Commit();

                return new ActionResult<ProdutosCategorias>(produtosCategorias);
          
        }

        public ActionResult<ProdutosCategorias> Update(ProdutosCategorias produtosCategorias)
        {

            ProdutosCategoriasValidator validator = new ProdutosCategoriasValidator();
            ValidationResult results = validator.Validate(produtosCategorias);
            if (!_ProdutoRepository.Query().Any(x => x.ProdutosCategoriaId == produtosCategorias.ProdutosCategoriaId))
            {
                throw new Exception("Registro não encontrado");
            }
            else if (!results.IsValid)
            {
                string allMessages = results.ToString(" -- ");
                return new ActionResult<ProdutosCategorias>(allMessages);
            }

            _ProdutoRepository.Update(produtosCategorias);

            _ProdutoRepository.Commit();

            return new ActionResult<ProdutosCategorias>(produtosCategorias);

        }
    }
}