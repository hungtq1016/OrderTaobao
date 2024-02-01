using Core;
using System.Text.Json.Serialization;

namespace OAuth2Service.Models
{
    public class Role: Entity
    {
        public string Name { get; set; }
        public string Note { get; set; }

        public ICollection<Assignment> Assignments { get; set; }
        [JsonIgnore]
        public ICollection<Group> Groups { get; set; }
    }
}
