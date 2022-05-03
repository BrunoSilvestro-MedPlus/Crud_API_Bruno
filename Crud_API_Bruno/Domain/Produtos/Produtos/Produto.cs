using Crud_API_Bruno.Domain.Produtos.ProdutoImagem;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Crud_API_Bruno.Domain.Products
{
    public class Produto
    {
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Informe o nome do produto")]
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        //public IFormFile Imagem { get; set; }
        public int? CodigoBarra { get; set; }

        [Required(ErrorMessage = "Informe a data")]
        public DateTime Data { get; set; }
        public double? Altura { get; set; }
        public double? Largura { get; set; }
        public double? Comprimento { get; set; }

        public ProdutoImagem ProdutoImagem { get; set; }
        public ICollection<ProdutosCategorias> ProdutosCategorias { get; set; }
    }

    public class ProdutoValidator : AbstractValidator<Produto>
    {

        public ProdutoValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("nome n�o pode ser vazio")
            .Length(1, 100).WithMessage("nome n�o pode possuir mais de 100 caracteres");

            RuleFor(x => x.Data)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Data inv�lida maior que a atual");


            RuleFor(x => x.Preco)
                .NotEqual(0).WithMessage("O valor n�o pode ser zero");
        }

    }
}