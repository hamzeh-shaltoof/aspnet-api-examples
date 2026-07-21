

using System.Net.Http.Headers;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using ResponseCaching.Requests;
using ResponseCaching.Responses;
using ResponseCaching.Services;
using Microsoft.AspNetCore.Mvc;

namespace ResponseCaching.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    [ResponseCache(Duration = 30 , Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
    {
        Console.WriteLine("Controller Action visited");

        var PagedResult = await productService.GetProductsAsync(page, pageSize);

        return Ok(PagedResult);
    }

    [HttpGet("{productId:int}", Name = nameof(GetById))]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any , VaryByHeader = "If-None-Match")]
    public async Task<ActionResult<ProductResponse>> GetById(int productId)
    {
        var product = await productService.GetProductByIdAsync(productId);

        if (product is null)
            return NotFound($"Product with Id '{productId}' not found");

        var etag = GenerateEtag(product);
        Console.WriteLine("Controller Action visited");

        if (Request.Headers.IfNoneMatch == etag)
            return StatusCode(StatusCodes.Status304NotModified);

        Response.Headers.ETag = new EntityTagHeaderValue(etag).ToString();


        return Ok(product);
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

        return NoContent();
    }

    private string GenerateEtag(ProductResponse product)
    {
        var raw = $"{product.ProductId}|{product.Name}|{product.Price}";
        var bytes = Encoding.UTF8.GetBytes(raw);
        var hash = SHA256.HashData(bytes);

        return $"\"{Convert.ToBase64String(hash)}\"";

    }

}