using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities.Order_Aggregate;

namespace talabat.Core.ServicesContext
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string BuyerEmail,string BasketId,int DeliveryMethodID, Address ShippingAddress);

        Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail);
        Task<Order> GetOrderByIdForSpecificUserAsync(string BuyerEmail, int OrderId);




    }
}
