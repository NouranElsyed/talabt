﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talabat.Core.Entities.Order_Aggregate
{
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrder product,  int quantity, decimal price)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public  ProductItemOrder Product { get; set; } 
        public decimal Price{ get; set; }
        public int Quantity { get; set; }

    }

}
