namespace assignment_3_api.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal RowPrice // gets the total price for each orderrow
        {
            get
            {
                return Price * Quantity;
            }
        }

    }
}
