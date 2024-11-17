using System.ComponentModel.DataAnnotations;
using talabat.Core.Entities.Order_Aggregate;

namespace talabtAPIs.DTOs
{
    public class OrderDTO
    {
        [Required]
        public string basketId { get; set; }
        [Required]
        public int deliveryMethodId { get; set; }
        [Required]
        public AddressDTO shipToAddress { get; set; }


    }
}
