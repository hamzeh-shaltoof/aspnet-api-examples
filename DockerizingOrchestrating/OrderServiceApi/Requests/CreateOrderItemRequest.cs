namespace M02.OrderPaymentSystem.OrderServiceApi.Requests;

public class CreateOrderItemRequest
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}