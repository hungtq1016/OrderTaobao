namespace AddressService.Models
{
    public class Province : BaseAddress
    {
        public ICollection<District>? Districts { get; } = new List<District>();

    }
}
