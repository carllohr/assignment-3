using assignment_3_api.Models.Entities;
using System.Collections.ObjectModel;

namespace assignment_3_api.Models
{
    public class OrderRequest
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public ObservableCollection<ProductModel> Products { get; set; } = null!;  
        public decimal TotalPrice { get; set; } 
    }
}
