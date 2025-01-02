public class Payment
{
    public int Id { get; set; } // Unique identifier for the payment method
    public string UserId { get; set; } // ID of the patient (linked to the Patient class)
    public string CardNumber { get; set; } // Encrypted card number
    public string ExpiryDate { get; set; } // Card expiry date
    public string CardHolderName { get; set; } // Name on the card
}
