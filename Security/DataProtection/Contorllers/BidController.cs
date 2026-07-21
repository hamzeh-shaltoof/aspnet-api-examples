using DataProtection.Requests;
using DataProtection.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataProtection.Contorllers
{
    [ApiController]
    [Route("api/bid")]
    public class BidController(IBidingService bidingService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllBid() {
            var bids = await bidingService.GetAllBidsAsync();
            return Ok(bids);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBidById(Guid id) {

            var bid = await bidingService.GetBidByIdAsync(id);
            return Ok(bid);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(BidRequest request) {

            var createdBid = await bidingService.CreateBidAsync(request);

            return Created($"/api/bid/{createdBid.Id}", createdBid);
        }
    }
}
