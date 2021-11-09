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
    public class MunicipioMap : IEntityTypeConfiguration<MunicipioEntity>
    {
        public void Configure(EntityTypeBuilder<MunicipioEntity> builder)
        {
            builder.ToTable("Municipio");
            builder.HasKey(key => key.Id);
            builder.HasIndex(index => index.CodigoIbge).IsUnique();
            builder.HasOne(fk => fk.Uf).WithMany(fk => fk.Municipios);
        }
    }
}
