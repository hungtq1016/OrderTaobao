﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("ADDRESS")]
    public class Address : BaseEntity
    {

        [Column("STREET", TypeName = "nvarchar"), MaxLength(100)]
        public string Street { get; set; } = string.Empty;

        [Column("WARD_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Ward")]
        public string WardId { get; set; }
        public Ward Ward { get; set; } = null!;
    }
}
