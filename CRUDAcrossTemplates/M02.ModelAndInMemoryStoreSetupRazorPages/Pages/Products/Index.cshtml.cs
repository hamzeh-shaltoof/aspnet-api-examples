using M02.ModelAndInMemoryStoreSetupRazorPages.Model;
using M02.ModelAndInMemoryStoreSetupRazorPages.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M02.ModelAndInMemoryStoreSetupRazorPages.Pages.Products
{
    public class IndexModel(ProductStore store) : PageModel
    {
        public IEnumerable<Product> Products { get; set; }

        public void OnGet()
        {
             NotFound();
            Products = store.GetAll();
        }

        public IActionResult OnGetDelete(Guid id)
        {
            var product = store.GetById(id);
            if (product != null)
            {
                store.Delete(product);
            }
            return RedirectToPage("Index");
        }
    }
}