using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_API_Bruno.Application.ViewModels
{
    public class ProdutoImagemVM
    {
        public int ProdutoId { get; set; }
        public IFormFile Image { get; set; }
    }
}
