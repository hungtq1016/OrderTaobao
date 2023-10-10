﻿
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseSource.Model
{
    [Table("ADDRESS")]
    public class Address : BaseEntity
    {

        [Column("STREET", TypeName = "nvarchar"), MaxLength(100)]
        public string Street { get; set; } = string.Empty;

        [Column("WARD_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Ward")]
        public Guid WardId { get; set; }
        public Ward Ward { get; set; } = null!;
    }
}
