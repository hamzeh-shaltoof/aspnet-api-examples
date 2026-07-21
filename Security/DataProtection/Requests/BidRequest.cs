namespace DataProtection.Requests
{
    public class BidRequest
    {
        public decimal Amount { get; set; }
        public DateTime BidDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Telephone { get; set; }
        public string? Address { get; set; }
    }
}