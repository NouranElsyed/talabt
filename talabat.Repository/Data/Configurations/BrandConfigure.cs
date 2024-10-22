using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities;

namespace talabat.Repository.Data.Configurations
{
    public class BrandConfigure  :    IEntityTypeConfiguration<Brand>
    {
   
            public void Configure(EntityTypeBuilder<Brand> builder)
            {
                builder.Property(B => B.Name).IsRequired();
            }
        
    }
}
