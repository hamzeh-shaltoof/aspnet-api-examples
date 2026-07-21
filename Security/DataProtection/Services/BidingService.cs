using DataProtection.Data;
using DataProtection.Entities;
using DataProtection.Requests;
using DataProtection.Responses;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace DataProtection.Services
{
    public class BidingService(AppDbContext context , IDataProtectionProvider  protectionProvider) : IBidingService
    {
        private readonly IDataProtector _protector = protectionProvider.CreateProtector("Bidding.Protection");
        public async Task<BidReponse> CreateBidAsync(BidRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var bid = new Bid()
            {
               FirstName = string.IsNullOrWhiteSpace(request.FirstName!) ?  null : _protector.Protect(request.FirstName!),
               LastName = string.IsNullOrWhiteSpace(request.LastName!) ? null : _protector.Protect( request.LastName!),
               Amount = request.Amount,
               Email = string.IsNullOrWhiteSpace(request.Email!) ? null : _protector.Protect(request.Email!),
               BidDate  = request.BidDate,
               Address = string.IsNullOrWhiteSpace(request.Address!) ? null : _protector.Protect(request.Address!),
               Telephone = string.IsNullOrWhiteSpace(request.Telephone!) ? null : _protector.Protect(request.Telephone!),
            };
          await context.AddAsync(bid);

          await context.SaveChangesAsync();

          return BidReponse.FromModel(bid,_protector);
         
        }

        public async Task<List<BidReponse>> GetAllBidsAsync()
        {
            var bids = context.Bids.OrderByDescending(x => x.BidDate);
            return await bids.Select(bid => BidReponse.FromModel(bid, _protector)).ToListAsync();
        }

        public async Task<BidReponse> GetBidByIdAsync(Guid id)
        {
            var bid = await context.Bids.FindAsync(id);

            ArgumentNullException.ThrowIfNull(bid);

            return  BidReponse.FromModel(bid, _protector);

        }
    }
}
