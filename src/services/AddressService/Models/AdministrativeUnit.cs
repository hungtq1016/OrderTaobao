using Core;

namespace AddressService.Models
{
    public class AdministrativeUnit : Entity
    {
        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public ICollection<Ward>? Wards { get; } = new List<Ward>();

        public ICollection<District>? Districts { get; } = new List<District>();

        public ICollection<Province>? Provinces { get; } = new List<Province>();
    }
}
