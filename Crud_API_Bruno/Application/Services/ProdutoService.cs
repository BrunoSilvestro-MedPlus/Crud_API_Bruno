using System.Threading;
using System;
using System.Collections.Generic;
using Crud_API_Bruno.Application.ViewModels;
using Crud_API_Bruno.Domain.Products;
using System.Linq;
using FluentValidation.Results;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Crud_API_Bruno.Application.Products
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _productRepository;

        public ProdutoService(IProdutoRepository ProductRepository)
        {
            _productRepository = ProductRepository;
        }
        public ActionResult Remove(int id)
        {
            // Validações pré exclusão
            try
            {
                _productRepository.Delete(id);

                _productRepository.Commit();

                return new ActionResult();
            }
            catch (Exception e)
            {
                return new ActionResult(e.Message);
            }
        }

        public IEnumerable<Produto> FindAll()
        {
            return _productRepository.Query();
        }

        public Produto FindOne(int id)
        {
            return _productRepository.FindById(id);
        }

        public bool Exists(int id)
        {
            return _productRepository.Query().Any(x => x.ProdutoId == id);
        }

        public ActionResult<Produto> Insert(Produto produto)
        {
            {
                ProdutoValidator validator = new ProdutoValidator();
                ValidationResult results = validator.Validate(produto);
                if (!results.IsValid)
                {
                    string allMessages = results.ToString(" -- ");
                    return new ActionResult<Produto>(allMessages);
                }
                

                    _productRepository.Add(produto);

                    _productRepository.Commit();

                    return new ActionResult<Produto>(produto);

            }
         
        }

        public ActionResult<Produto> Update(Produto produto)
        {
        
                if (!_productRepository.Query().Any(x => x.ProdutoId == produto.ProdutoId))
                {
                    return new ActionResult<Produto>("Registro não encontrado");
                }

                ProdutoValidator validator = new ProdutoValidator();
                ValidationResult results = validator.Validate(produto);
                if (!results.IsValid)
                {
                    string allMessages = results.ToString(" -- ");
                    return new ActionResult<Produto>(allMessages);
                }

                _productRepository.Update(produto);

                _productRepository.Commit();

                return new ActionResult<Produto>(produto);
        
        }     
    }
}