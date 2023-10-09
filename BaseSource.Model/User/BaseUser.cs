
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseSource.Model
{
    public abstract class BaseUser 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "varchar"), MaxLength(36)]
        public Guid Id { get; set; }

        [Column("USER_NAME",TypeName ="nvarchar"),MaxLength(36)]
        public string UserName { get; set; } = string.Empty;

        [Column("PASSWORD")]
        public string Password { get; set; } = string.Empty;

        [Column("FIRST_NAME", TypeName = "nvarchar"), MaxLength(36)]
        public string FirstName { get; set; } = string.Empty;

        [Column("LAST_NAME", TypeName = "nvarchar"), MaxLength(36)]
        public string LastName { get; set; } = string.Empty;

        [Column("EMAIL", TypeName = "nvarchar"), MaxLength(255)]
        [Required]
        public string Email { get; set; } = string.Empty;

        [Column("PHONE", TypeName = "varchar"), MaxLength(15)]
        [Required]
        public string Phone { get; set; } = string.Empty;

       /* [Column("ADDRESS_ID", TypeName = "varchar"), MaxLength(36)]
        public Guid AddressId { get; set; }
        public Address Address { get; set; } = null!;*/
    }
}
