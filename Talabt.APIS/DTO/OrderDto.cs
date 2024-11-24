using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.identity;

namespace Talabt.APIS.DTO
{
    public class OrderDto
    {
        [Required]
       
       public int DeliveryMethodId { get; set; }
        [Required]

        public string BasketId { get; set; }
        [Required]

        public AddressDTO Shippingaddress { get; set; }
    }
}
