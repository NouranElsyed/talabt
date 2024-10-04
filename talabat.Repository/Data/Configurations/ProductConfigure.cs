using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities;

namespace talabat.Repository.Data.Configurations
{
    internal class ProductConfigure : IEntityTypeConfiguration<Products>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Products> builder)
        {
            builder.Property(P => P.Name).IsRequired().HasMaxLength(60);
            builder.Property(P => P.Description).IsRequired();
            builder.Property(P => P.PictureUrl).IsRequired();
            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(P => P.Category).WithMany().HasForeignKey(P => P.CategoryId);
            builder.HasOne(P => P.Brands).WithMany().HasForeignKey(P => P.BrandsId).HasConstraintName("BrandId"); ;

        }
    }
}
