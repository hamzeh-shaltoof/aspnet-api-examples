namespace ElectronicCommerce.Enums
{
    public enum TransactionType
    {
        Payment,     // Funds have been successfully received and verified
        PartiallyRefunded,
        Refunded     // Amount has been returned to the customer's account
    }
}
