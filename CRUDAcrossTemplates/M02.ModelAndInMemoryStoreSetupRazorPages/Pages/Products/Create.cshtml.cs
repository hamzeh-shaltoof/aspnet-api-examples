using M02.ModelAndInMemoryStoreSetupRazorPages.Model;
using M02.ModelAndInMemoryStoreSetupRazorPages.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M02.ModelAndInMemoryStoreSetupRazorPages.Pages.Products
{
    public class CreateModel(ProductStore store) : PageModel
    {
            [BindProperty]
             public Product Product { get; set; }
   
        public IActionResult OnPost(Product product) {
            store.Add(product);
            return RedirectToPage("Index"); // return aother page 
            

        }
    }
}
