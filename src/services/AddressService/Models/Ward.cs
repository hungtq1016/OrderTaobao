namespace AddressService.Models
{
    public class Ward : BaseAddress
    {
        public Guid? DistrictId { get; set; }
        public District District { get; set; } = null!;
        public ICollection<Address>? Address { get; } = new List<Address>();

    }
}
