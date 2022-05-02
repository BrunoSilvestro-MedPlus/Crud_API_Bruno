using System;
using System.Collections.Generic;
using Crud_API_Bruno.Application.ViewModels;

namespace Crud_API_Bruno.Domain.Products
{
    public interface IProdutoService
    {
        ActionResult<Produto> Insert(Produto source);
        ActionResult<Produto> Update(Produto source);
        ActionResult Remove(int id);
        IEnumerable<Produto> FindAll();
        Produto FindOne(int id);
        bool Exists(int id);
    }
}