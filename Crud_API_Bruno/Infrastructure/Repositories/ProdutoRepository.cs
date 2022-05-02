using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Crud_API_Bruno.Domain.Products;
using Crud_API_Bruno.Infrastructure.Context;

namespace Crud_API_Bruno.Infrastructure.Repositories
{
    public class ProdutoRepository : AbstractRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MainContext context) : base(context)
        {

        }
        public override Produto FindById(int id)
        {
            return DbSet
                .Include(x => x.ProdutosCategorias)
                    .ThenInclude(x => x.Categoria)                    
                        .FirstOrDefault(x => x.ProdutoId == id);
        }
        public override IQueryable<Produto> Query()
        {
            return DbSet
            .Include(x => x.ProdutosCategorias)
                .ThenInclude(x => x.Categoria)                    
                    .AsNoTracking();
        }

    }
}