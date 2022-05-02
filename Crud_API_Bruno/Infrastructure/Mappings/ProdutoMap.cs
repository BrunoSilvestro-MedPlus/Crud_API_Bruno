using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Crud_API_Bruno.Domain.Products;

namespace Crud_API_Bruno.Infrastructure.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.Property(e => e.Data).HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.Descricao).IsRequired();

            //builder.Property(e => e.Imagem).HasColumnType("image");

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
