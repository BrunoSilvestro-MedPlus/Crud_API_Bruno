using Crud_API_Bruno.Domain.Products;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_API_Bruno.Domain.Produtos.ProdutoImagem
{
    public class ProdutoImagem
    {
        public int ProdutoImagemId { get; set; }
        public int ProdutoId { get; set; }

        public string CaminhoImagem { get; set; }


    }     

}
