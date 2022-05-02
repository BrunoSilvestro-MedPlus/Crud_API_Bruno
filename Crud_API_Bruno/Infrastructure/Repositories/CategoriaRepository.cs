using Crud_API_Bruno.Domain.Products;
using Crud_API_Bruno.Infrastructure.Context;

namespace Crud_API_Bruno.Infrastructure.Repositories
{
    public class CategoriaRepository : AbstractRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(MainContext context) : base(context)
        {

        }
    }
}