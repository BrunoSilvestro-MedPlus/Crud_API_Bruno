using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Crud_API_Bruno.Domain.Products
{
    public class ProdutosCategorias
    {
        public int ProdutosCategoriaId { get; set; }
        public int CategoriaId { get; set; }
        public int ProdutoId { get; set; }

        //Navigation property
        [JsonIgnore]

        public Categoria Categoria { get; set; }
        public Produto Produto { get; set; }
    }

    public class ProdutosCategoriasValidator : AbstractValidator<ProdutosCategorias>
    {

        public ProdutosCategoriasValidator()
        {
            RuleFor(x => x.CategoriaId)
                .NotNull().WithMessage("Categoria não pode ser nula")
                .NotEmpty().WithMessage("Categoria não pode ser vazia");

            RuleFor(x => x.ProdutoId)
                .NotNull().WithMessage("Produto não pode ser nula")
                .NotEmpty().WithMessage("Produto não pode ser vazia");
        }

    }
}