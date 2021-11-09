using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class CepMap : IEntityTypeConfiguration<CepEntity>
    {
        public void Configure(EntityTypeBuilder<CepEntity> builder)
        {
            builder.ToTable("Cep");
            builder.HasKey(key => key.Id);
            builder.HasIndex(key => key.Cep);
            builder.HasOne(fk => fk.Municipio).WithMany(fk => fk.Ceps);
        }
    }
}
