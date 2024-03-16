namespace AudioService.DTOs
{
    public class CollectionRequest : EntityRequest
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool Enable { get; set; }
    }
}
