namespace AddressService.Models
{
    public class Address : BaseAddress
    {
        public string Street { get; set; } = string.Empty;
        public string WardId { get; set; }
        public Ward? Ward { get; set; } = null!;
    }
}
