namespace JoHealth.Models
{
    public class Medicine
    {
        public int Id { get; set; } // Unique identifier for the medicine
        public string Name { get; set; } // Name of the medicine
        public string Description { get; set; } // Description of the medicine
        public decimal Price { get; set; } // Price of the medicine
        public int QuantityAvailable { get; set; } // Quantity available in stock
        public string ImageUrl { get; set; } // URL or path to the image of the medicine
        public bool IsOnSale { get; set; } // Indicates if the medicine is on sale
        public decimal SalePrice { get; set; } // Discounted price if on sale

        // Method to calculate the displayed price
        public decimal GetDisplayedPrice()
        {
            return IsOnSale ? SalePrice : Price;
        }

        // Method to decrease stock when purchased
        public void ReduceStock(int quantity)
        {
            if (quantity > QuantityAvailable)
                throw new InvalidOperationException("Not enough stock available.");
            QuantityAvailable -= quantity;
        }
    }
}
