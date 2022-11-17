using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_3.Models
{
    public class OrderRequest
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public ObservableCollection<ProductModel>? Products { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
