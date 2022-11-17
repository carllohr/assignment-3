using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace assignment_3_api.Models.Entities
{
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Created { get; set; } = DateTime.Now;
        [Required]
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }


        public ICollection<OrderRowEntity> OrderRows { get; set; }
    }
}
