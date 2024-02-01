using Core;
using System.Text.Json.Serialization;

namespace OAuth2Service.Models
{
    public class Permission: Entity
    {
        public string Type { get; set; }
        public string Value { get; set; }

        [JsonIgnore]
        public ICollection<Assignment> Assignments { get; set; }
    }
}
