using Domain.Entities;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<UsersEntity>
    {
        public void Configure(EntityTypeBuilder<UsersEntity> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(p => p.Id);
            builder.HasIndex(i => i.Email).IsUnique();
            builder.Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(60);
            builder.Property(user => user.Email)
                    .HasMaxLength(100);
        }
    }
}
