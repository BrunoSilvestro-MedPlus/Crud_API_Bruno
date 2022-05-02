using Crud_API_Bruno.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_API_Bruno.Domain.Produtos.ProdutoImagem
{
   public interface IProdutoImagemService
    {
        ActionResult<ProdutoImagem> Insert(ProdutoImagem source);
        ActionResult<ProdutoImagem> Update(ProdutoImagem source);
        ActionResult Remove(int id);
        IEnumerable<ProdutoImagem> FindAll();
        ProdutoImagem FindOne(int id);
      
    }
}
