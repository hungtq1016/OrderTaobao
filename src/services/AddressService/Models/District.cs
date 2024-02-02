namespace AddressService.Models
{
    public class District : BaseAddress
    {
        public Guid ProvinceId { get; set; }
        public Province? Province { get; set; } = null!;
        public ICollection<Ward>? Wards { get; } = new List<Ward>();
    }
}
