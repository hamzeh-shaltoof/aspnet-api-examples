using DataProtection.Entities;
using DataProtection.Requests;
using DataProtection.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DataProtection.Services
{
    public interface IBidingService
    {
        public Task<List<BidReponse>> GetAllBidsAsync();
        public Task<BidReponse> GetBidByIdAsync(Guid id);
        public Task<BidReponse> CreateBidAsync(BidRequest request);

    }
}