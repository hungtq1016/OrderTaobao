
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BaseSource.Model
{
    public abstract class BaseUser : IdentityUser<Guid>
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "varchar"), MaxLength(36)]
        public override Guid Id { get; set; }

        public string Password { get; set; } = string.Empty;

        [Column("FIRST_NAME", TypeName = "nvarchar"), MaxLength(36)]
        public string FirstName { get; set; } = string.Empty;

        [Column("LAST_NAME", TypeName = "nvarchar"), MaxLength(36)]
        public string LastName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        /* [Column("ADDRESS_ID", TypeName = "varchar"), MaxLength(36)]
         public Guid AddressId { get; set; }
         public Address Address { get; set; } = null!;*/
    }
}
