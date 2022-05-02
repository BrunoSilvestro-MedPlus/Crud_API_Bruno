using Crud_API_Bruno.Domain.Produtos.ProdutoImagem;
using Crud_API_Bruno.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_API_Bruno.Infrastructure.Repositories
{
    public class ProdutoImagemRepository : AbstractRepository<ProdutoImagem>, IProdutoImagemRepository
    {
        public ProdutoImagemRepository(MainContext context) : base(context)
        {

        }
    }
}
