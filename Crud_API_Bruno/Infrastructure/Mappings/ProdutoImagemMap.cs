using Crud_API_Bruno.Domain.Produtos.ProdutoImagem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_API_Bruno.Infrastructure.Mappings
{
    public class ProdutoImagemMap : IEntityTypeConfiguration<ProdutoImagem>
    {
        public void Configure(EntityTypeBuilder<ProdutoImagem> builder)
        {
            builder
                .ToTable(nameof(ProdutoImagem));

            builder
                .HasKey(x => x.ProdutoImagemId);

            builder.Property(e => e.ProdutoId)
                               .IsRequired();

           
        }
    }
}
