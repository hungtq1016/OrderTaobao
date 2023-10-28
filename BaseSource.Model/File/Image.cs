
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("IMAGES")]
    public class Image : BaseEntity
    {
        [DataMember]
        [Column("LABEL")]
        public string Label { get; set; }
        [Column("URL")]
        public string Url { get; set; }
        [Column("TYPE")]
        public string Type { get; set; }
        [Column("SIZE")]
        public UInt64 Size { get; set; }
        [JsonIgnore]
        public List<ImageUser> Users { get; } = new();
    }
}
