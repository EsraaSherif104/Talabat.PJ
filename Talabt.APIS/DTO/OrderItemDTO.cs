using Talabat.Core.Entities.Core_Aggra;

namespace Talabt.APIS.DTO
{
    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}