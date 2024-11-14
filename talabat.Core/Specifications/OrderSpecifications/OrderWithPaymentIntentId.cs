using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities.Order_Aggregate;

namespace talabat.Core.Specifications.OrderSpecifications
{
    public class OrderWithPaymentIntentId : BaseSpecifications<Order>
    {
        public OrderWithPaymentIntentId(string PaymentIntentId) : base(O => O.PaymentIntentId == PaymentIntentId)
        {

        }

    }
}
