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

        public decimal GetDisplayedPrice()
        {
            return IsOnSale ? SalePrice : Price;
        }

        public void ReduceStock(int quantity)
        {
            if (quantity > QuantityAvailable)
                throw new InvalidOperationException("Not enough stock available.");
            QuantityAvailable -= quantity;
        }
    }
}
