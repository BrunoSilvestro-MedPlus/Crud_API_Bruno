using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Crud_API_Bruno.Domain.Products
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        
        [Required(ErrorMessage = "Informe o nome da Categoria")]
        public string CategoriaNome { get; set; }
        


        //Navigation property
        [JsonIgnore]
        public ICollection<ProdutosCategorias> ProdutosCategorias { get; set; }
    }

    public class CategoriaValidator : AbstractValidator<Categoria>
    {

        public CategoriaValidator()
        {
            RuleFor(x => x.CategoriaNome).NotEmpty().WithMessage("nome não pode ser vazio")
                .NotNull().WithMessage("nome não pode ser nulo")
                .Length(1, 100).WithMessage("nome não pode possuir mais de 100 caracteres");

        }

    }

}

