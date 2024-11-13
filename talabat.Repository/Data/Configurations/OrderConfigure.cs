using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities.Order_Aggregate;

namespace talabat.Repository.Data.Configurations
{
    public class OrderConfigure : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.Status)
                .HasConversion(OStatus => OStatus.ToString(),
                OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus));
            builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");
            builder.OwnsOne(O => O.ShippingAddress, shippingAddress => shippingAddress.WithOwner());
            builder.HasOne(O =>O.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.NoAction);
 
        }
    }
}
