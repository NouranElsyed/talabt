using talabat.Core.Entities;

namespace talabtAPIs.DTOs
{
    public class CustomerBasketDTO
    {
        public string Id { get; set; }
        public List<BasketItemDTO> items { get; set; }
    }
}
