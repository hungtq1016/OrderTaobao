namespace AudioService.Models
{
    public class Collection : Entity
    {
        public string Name { get; set; }
        public string Slug { get; set; }

        public ICollection<Album> Albums { get; } = new List<Album>();
    }
}
