using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talabat.Core.Entities.Order_Aggregate
{
    public class DeliveryMethod : BaseEntity
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string Description, string deliveryTime, decimal cost)
        {
            ShortName = shortName;
            Description = Description;
            DeliveryTime = deliveryTime;
            Cost = cost;
        }

        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Cost { get; set; }


    }
}
