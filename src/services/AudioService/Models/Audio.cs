using System.Text.Json.Serialization;

namespace AudioService.Models
{
    public class Audio : AbstractFile
    {
        [JsonIgnore]
        public ICollection<Album> Albums { get; } = new List<Album>();
    }
}
