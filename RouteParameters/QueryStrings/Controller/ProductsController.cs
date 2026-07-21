using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QueryStrings.Controller
{
    public class ProductsController : ControllerBase
    {
        [HttpGet("/products/Phones-Controller")]
        public IActionResult Pagination (int  page , int pageSize)
        {
            return Ok($"Page = {page} , Page Size = {pageSize}");
        }

        [HttpGet("/PaginationAsClass")]
        public IActionResult PaginationAsClass([FromQuery] SearchParameter searchParameter)
        {
            return Ok($"Query = {searchParameter.Query} , " +
                      $"Page = {searchParameter.Page} , " +
                      $"Page Size = {searchParameter.PageSize}");
        }
    }
}
