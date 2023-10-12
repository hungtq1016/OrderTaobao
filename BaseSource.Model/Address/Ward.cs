﻿
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseSource.Model
{
    [Table("WARDS")]
    public class Ward : BaseAddress
    {
        [Column("DISTRICT_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("District")]
        public Guid DistrictId { get; set; }
        public District District { get; set; } = null!;
        public ICollection<Address> Address { get; } = new List<Address>();

    }
}