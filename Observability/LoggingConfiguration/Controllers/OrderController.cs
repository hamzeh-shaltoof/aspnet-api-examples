using LoggingConfiguration.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoggingConfiguration.Controllers
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
