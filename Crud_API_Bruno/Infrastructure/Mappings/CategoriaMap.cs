using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Crud_API_Bruno.Domain.Products;

namespace Crud_API_Bruno.Infrastructure.Mappings
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder
                .ToTable(nameof(Categoria));

            builder
                .HasKey(x => x.CategoriaId);

            builder.Property(e => e.CategoriaNome)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
