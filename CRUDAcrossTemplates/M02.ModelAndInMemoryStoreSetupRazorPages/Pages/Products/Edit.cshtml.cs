using M02.ModelAndInMemoryStoreSetupRazorPages.Model;
using M02.ModelAndInMemoryStoreSetupRazorPages.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M02.ModelAndInMemoryStoreSetupRazorPages.Pages.Products
{
    public class EditModel(ProductStore store) : PageModel
    {
        [BindProperty]
        public Product Product { get; set; }

        public IActionResult OnGet(Guid id)
        {
            Product = store.GetById(id);
            if (Product == null)
                return NotFound();
            return Page();
        }
        public IActionResult OnPost()
        {
            store.Update(Product);
            return RedirectToPage("Index");
        }
    }
}
