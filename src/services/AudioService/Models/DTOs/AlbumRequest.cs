namespace AudioService.DTOs
{
    public class AlbumRequest : EntityRequest
    {
        public Guid CollectionId { get; set; }
        public Guid AudioId { get; set; }
        public bool Enable { get; set; }
    }
}
