namespace ElectronicCommerce.Enums
{
    public enum OrderStatus
    {
        Pending,      // Order submitted by customer, awaiting review or approval
        Confirmed,    // Order approved by admin and ready for processing
        Processing,   // Items are being picked and packed in the warehouse
        Shipped,      // Order has been handed over to the delivery company
        Delivered,    // Order has been successfully delivered to the customer
        Completed,    // Order lifecycle is finished (e.g., after return period expires)
        Cancelled,    // Order was cancelled by the customer or the admin
        Returned      // Customer returned the product after delivery
    }
}
