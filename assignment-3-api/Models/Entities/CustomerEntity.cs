using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace assignment_3_api.Models.Entities
{
    public class CustomerEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; } = null!;
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; } = null!;
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; } = null!;
        public ICollection<OrderEntity> Orders { get; set; } = null!;
    }
}
