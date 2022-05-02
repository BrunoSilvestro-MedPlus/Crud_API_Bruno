using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Crud_API_Bruno.Domain.Products;

namespace Crud_API_Bruno.Infrastructure.Mappings
{
    public class ProdutosCategoriasMap : IEntityTypeConfiguration<ProdutosCategorias>
    {
        public void Configure(EntityTypeBuilder<ProdutosCategorias> builder)
        {
            builder
                .ToTable(nameof(ProdutosCategorias));

            builder
                .HasKey(x => x.ProdutosCategoriaId);

            builder.Property(e => e.ProdutoId)
                               .IsRequired();

            builder.Property(e => e.CategoriaId)
                .IsRequired();
        }
    }
}
