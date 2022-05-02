using Crud_API_Bruno.Domain.Products;
using Crud_API_Bruno.Infrastructure.Context;

namespace Crud_API_Bruno.Infrastructure.Repositories
{
    public class ProdutosCategoriasRepository : AbstractRepository<ProdutosCategorias>, IProdutosCategoriasRepository
    {
        public ProdutosCategoriasRepository(MainContext context) : base(context)
        {

        }
    }
}