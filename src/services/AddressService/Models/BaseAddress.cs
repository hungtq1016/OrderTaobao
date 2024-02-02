using Core;

namespace AddressService.Models
{
    public abstract class BaseAddress : Entity
    {
        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string NameEn { get; set; } = string.Empty;

        public string EnSlug { get; set; } = string.Empty;

        public int CODE { get; set; }

        public Guid AdministrativeUnitID { get; set; }
        public AdministrativeUnit? AdministrativeUnit { get; set; }
    }
}
