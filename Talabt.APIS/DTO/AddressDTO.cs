using Talabat.Core.Entities.identity;

namespace Talabt.APIS.DTO
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }

    }
}
