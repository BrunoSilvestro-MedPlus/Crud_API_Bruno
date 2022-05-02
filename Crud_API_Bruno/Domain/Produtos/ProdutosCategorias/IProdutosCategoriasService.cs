using System;
using System.Collections.Generic;
using Crud_API_Bruno.Application.ViewModels;

namespace Crud_API_Bruno.Domain.Products
{
    public interface IProdutosCategoriasService
    {
        ActionResult<ProdutosCategorias> Insert(ProdutosCategorias source);
        ActionResult<ProdutosCategorias> Update(ProdutosCategorias source);
        ActionResult Remove(int id);
        IEnumerable<ProdutosCategorias> FindAll();
        ProdutosCategorias FindOne(int id);
    }
}