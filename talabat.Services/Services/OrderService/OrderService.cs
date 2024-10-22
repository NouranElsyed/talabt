
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core;
using talabat.Core.Entities;
using talabat.Core.Entities.Order_Aggregate;
using talabat.Core.RepositoriesContext;
using talabat.Core.ServicesContext;

namespace talabat.Services.Services.OrderService
{

    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
    
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodID, Address ShippingAddress)
        {
            //1.Get Basket From Basket Repo
            var basket = await _basketRepository.GetBasketAsync(BasketId);
            //2.Get Selected Items at Basket From Product Repo
            var OrderItems= new List<OrderItem>();
            if (basket?.items.Count > 0) 
            {
                foreach (var item  in basket.items) 
                {
                    var product = await _unitOfWork.Repository<Products>().GetAsync(item.Id);
                    var ProductItemOrdered = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                    var OrderItem = new OrderItem(ProductItemOrdered,item.Quantity,product.Price);
                    OrderItems.Add(OrderItem);
                }
            }
           
            //3.Calculate SubTotal
            var subtotal = OrderItems.Sum(item => item.Price * item.Quantity);
            //4.Get Delivery Method From DeliveryMethod Repo
            var deliveryMethod =  await _unitOfWork.Repository<DeliveryMethod>().GetAsync(DeliveryMethodID);
            //5.Create Order
            var order = new Order(BuyerEmail, ShippingAddress, deliveryMethod, OrderItems, subtotal);
            //6.Add Order Locally
            await _unitOfWork.Repository<Order>().AddAsync(order);
            //7.Save Order To Database[ToDo]
            var Result = await _unitOfWork.CompleteAsync();
            if (Result <= 0) return null;
            return order;
        }

        public Task<Core.Entities.Order_Aggregate.Order> GetOrderByIdForSpecificUserAsync(string BuyerEmail, int OrderId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Core.Entities.Order_Aggregate.Order>> GetOrdersForSpecificUserAsync(string BuyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
