namespace MinimalAPIMediaTypeVersioning.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string? CurrentPrice { get; set; }
        public int Quantity { get; set; }
    }
}
