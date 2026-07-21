using LogLevels.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogLevels.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IOrderService Service) : ControllerBase
    {
        [HttpGet("{orderId:int}")]
        public IActionResult Get(int orderId)
        {
            return Ok(Service.GetOrders(orderId));    
        }
    }
}
