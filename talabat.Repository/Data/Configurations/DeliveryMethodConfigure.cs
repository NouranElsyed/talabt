﻿using Azure;
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
    public class DeliveryMethodConfigure:IEntityTypeConfiguration<DeliveryMethod>
    {
         
      
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
         
            builder.Property(D => D.Cost).HasColumnType("decimal(18,2)");
   
        }
    }
}
