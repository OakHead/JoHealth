namespace JoHealth.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public string ImageUrl { get; set; }
        public bool IsOnSale { get; set; }
        public decimal SalePrice { get; set; }

        public decimal DisplayPrice => IsOnSale ? SalePrice : Price;
    }
}
