namespace AudioService.DTOs
{
    public class AudioRequest : EntityRequest
    {
        public string Title { get; set; }
        public string? Alt { get; set; }
        public long Size { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public bool Enable { get; set; }
    }
}
