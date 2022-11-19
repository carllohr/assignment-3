namespace assignment_3_api.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal RowPrice
        {
            get
            {
                return Price * Quantity;
            }
        }

    }
}
