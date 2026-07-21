using DataProtection.Entities;
using Microsoft.AspNetCore.DataProtection;

namespace DataProtection.Responses
{
    public class BidReponse
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime BidDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Telephone { get; set; }
        public string? Address { get; set; }

        private BidReponse() { }

        public static BidReponse FromModel(Bid bid , IDataProtector protector)
        {
            ArgumentNullException.ThrowIfNull(bid);

            return new BidReponse()
            {
                Id = bid.Id,
                Amount = bid.Amount,
                FirstName = string.IsNullOrWhiteSpace(bid.FirstName) ? null : protector.Unprotect(bid.FirstName!),
                LastName = string.IsNullOrWhiteSpace(bid.LastName) ? null : protector.Unprotect(bid.LastName!),
                Email = string.IsNullOrWhiteSpace(bid.Email) ? null : protector.Unprotect(bid.Email!),
                Telephone = string.IsNullOrWhiteSpace(bid.Telephone) ? null : protector.Unprotect(bid.Telephone!),
                Address = string.IsNullOrWhiteSpace(bid.Address) ? null : protector.Unprotect(bid.Address!),
                BidDate = bid.BidDate,
            };
               
        }
            
    }
}
