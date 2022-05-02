using System.Threading;
using System;
using System.Collections.Generic;
using Crud_API_Bruno.Application.ViewModels;
using System.Linq;
using Crud_API_Bruno.Domain.Products;
using FluentValidation.Results;

namespace Crud_API_Bruno.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public ActionResult Remove(int id)
        {
            // Validações pré exclusão
            try
            {
                _categoriaRepository.Delete(id);

                _categoriaRepository.Commit();

                return new ActionResult();
            }
            catch (Exception e)
            {
                return new ActionResult(e.Message);
            }
        }

        public IEnumerable<Categoria> FindAll()
        {
            return _categoriaRepository.Query();
        }

        public Categoria FindOne(int id)
        {
            return _categoriaRepository.FindById(id);
        }

        public bool Exists(int id)
        {
            return _categoriaRepository.Query().Any(x => x.CategoriaId == id);
        }

        public ActionResult<Categoria> Insert(Categoria categoria)
        {
            CategoriaValidator validator = new CategoriaValidator();
            ValidationResult results = validator.Validate(categoria);
            if (!results.IsValid)
            {
                string allMessages = results.ToString(" -- ");
                return new ActionResult<Categoria>(allMessages);
            }

            _categoriaRepository.Add(categoria);

                _categoriaRepository.Commit();

                return new ActionResult<Categoria>(categoria);
           
        }

        public ActionResult<Categoria> Update(Categoria categoria)
        {
            CategoriaValidator validator = new CategoriaValidator();
            ValidationResult results = validator.Validate(categoria);
            if (!_categoriaRepository.Query().Any(x => x.CategoriaId == categoria.CategoriaId))
                {
                    return new ActionResult<Categoria>("Registro não encontrado");
            }
            else if (!results.IsValid)
            {
                string allMessages = results.ToString(" -- ");
                return new ActionResult<Categoria>(allMessages);

            }

                _categoriaRepository.Update(categoria);

                _categoriaRepository.Commit();

                return new ActionResult<Categoria>(categoria);
            
        }
    }
}