using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities.Order_Aggregate;

namespace talabat.Repository.Data.Configurations
{
    public class OrderItemConfigure : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(O => O.Price).HasColumnType("decimal(18,2)");
            builder.OwnsOne(O => O.Product, X => X.WithOwner());
        }
    }
}
