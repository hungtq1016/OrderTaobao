namespace AudioService.DTOs
{
    public class AlbumResponse : Entity
    {
        public Guid CollectionId { get; set; }
        public Guid AudioId { get; set; }
    }
}
