using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace assignment_3_api.Models.Entities
{
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        public ICollection<OrderRowEntity>? Rows { get; set; }
    }
}
