namespace ElectronicCommerce.Enums
{
    public enum PaymentStatus
    {
        Pending,                 // Payment process initiated but not yet finalized (Cash or Card)
        Paid,                   // Funds have been successfully received and verified
        Failed,                // Payment attempt was rejected or timed out
        PartiallyRefunded ,
        Refunded ,
        Canceled 
    }
}
