using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Crud_API_Bruno.Domain.Users;

namespace Crud_API_Bruno.Infrastructure.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.Property(x => x.Role)
                .HasConversion(new EnumToNumberConverter<Role, int>())
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(160)
                .IsRequired();
        }
    }
}
