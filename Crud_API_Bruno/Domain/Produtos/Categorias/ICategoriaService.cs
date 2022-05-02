using System;
using System.Collections.Generic;
using Crud_API_Bruno.Application.ViewModels;

namespace Crud_API_Bruno.Domain.Products
{
    public interface ICategoriaService
    {
        ActionResult<Categoria> Insert(Categoria categoria);
        ActionResult<Categoria> Update(Categoria categoria);
        ActionResult Remove(int id);
        IEnumerable<Categoria> FindAll();
        Categoria FindOne(int id);
        bool Exists(int id);
    }
}