using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QueryStrings.Parable;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();

app.MapGet("/products/Phones", ([FromQuery] int page , int pageSize) => $"Page = {page} , Page Size = {pageSize}");

app.MapGet("/PaginationAsClass-Minimal", ([AsParameters] SearchParameter searchParameter) =>
{
    return ($"Query = {searchParameter.Query} , " +
            $"Page = {searchParameter.Page} , " +
            $"Page Size = {searchParameter.PageSize}");
}
);

app.MapGet("/array", (int[] ids) => ids);

app.MapGet("/RangeDateQueryPare", (RangeDateQuery rangeDateQuery)
    => $"{rangeDateQuery.FromDate} - {rangeDateQuery.ToDate}");

app.MapControllers();
app.Run();


public class SearchParameter
{
    public string Query { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
