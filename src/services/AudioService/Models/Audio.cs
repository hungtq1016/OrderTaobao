namespace AudioService.Models
{
    public class Audio : AbstractFile
    {
        public ICollection<Album> Albums { get; } = new List<Album>();
    }
}
