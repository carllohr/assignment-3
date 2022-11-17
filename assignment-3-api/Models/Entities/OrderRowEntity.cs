using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace assignment_3_api.Models.Entities
{
    public class OrderRowEntity
    {
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; } = null!;
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; } = null!;
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
    }
}
