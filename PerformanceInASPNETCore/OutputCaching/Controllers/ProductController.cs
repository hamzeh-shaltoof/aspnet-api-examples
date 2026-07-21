using OutputCaching.Requests;
using OutputCaching.Responses;
using OutputCaching.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace OutputCaching.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService, IOutputCacheStore cache) : ControllerBase
{
    [HttpGet]
    [OutputCache(PolicyName = "Single-Product")]
    //[OutputCache(Duration = 20 , VaryByQueryKeys = ["page","pageSize"])]
    public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
    {
        Console.WriteLine("Controller Action visited");

        var PagedResult = await productService.GetProductsAsync(page, pageSize);

        return Ok(PagedResult);
    }

    [HttpGet("{productId:int}", Name = nameof(GetById))]
    [OutputCache(PolicyName = "Pagination")]
    //[OutputCache(Duration = 20 , VaryByRouteValueNames = ["productId"])]

    public async Task<ActionResult<ProductResponse>> GetById(int productId)
    {
        Console.WriteLine("Controller Action (Get By Id) visited");

        var response = await productService.GetProductByIdAsync(productId);
        if (response is null)
            return NotFound($"Product with Id '{productId}' not found");

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProductRequest request)
    {
        var response = await productService.AddProductAsync(request);
        return CreatedAtRoute(nameof(GetById), new { productId = response.ProductId }, response);
    }

    [HttpPut("{productId:int}")]
    public async Task<IActionResult> Put(int productId, [FromBody] UpdateProductRequest request)
    {
        await productService.UpdateProductAsync(productId, request);
        return NoContent();
    }

    [HttpDelete("{productId:int}")]
    public async Task<IActionResult> Delete(int productId)
    {
        await productService.DeleteProductAsync(productId);

        await cache.EvictByTagAsync("products",default);

        return NoContent();
    }
}