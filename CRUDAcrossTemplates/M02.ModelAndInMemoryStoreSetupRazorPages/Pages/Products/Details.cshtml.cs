using M02.ModelAndInMemoryStoreSetupRazorPages.Model;
using M02.ModelAndInMemoryStoreSetupRazorPages.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M02.ModelAndInMemoryStoreSetupRazorPages.Pages.Products
{
    public class DetailModel(ProductStore store) : PageModel
    {
        public Product? Product { get; private set; }

        public IActionResult OnGet(Guid id)
        {
            Product = store.GetById(id);
            return Page(); // return same page 
        }
    }
}
