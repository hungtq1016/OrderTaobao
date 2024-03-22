
using System.Text.Json.Serialization;

namespace AudioService.Models
{
    public class Album : Entity
    {
        public Guid CollectionId { get; set; }
        [JsonIgnore]
        public Collection Collection { get; set; }

        public Guid AudioId { get; set; }
        public Audio Audio { get; set; }
    }
}
