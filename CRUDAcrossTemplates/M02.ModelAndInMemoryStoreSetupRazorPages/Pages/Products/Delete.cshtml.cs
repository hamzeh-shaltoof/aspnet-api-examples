//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using M02.ModelAndInMemoryStoreSetupRazorPages.Store;

//namespace M02.ModelAndInMemoryStoreSetupRazorPages.Pages.Products
//{
//    // لاحظ أننا لا نحتاج لعمل أي شيء داخل ملف الـ .cshtml
//    public class DeleteModel(ProductStore store) : PageModel
//    {
//        public IActionResult OnGet(Guid id)
//        {
//            var product = store.GetById(id);
//            if (product != null)
//            {
//                store.Delete(product);
//            }

//            // بعد الحذف، نرجعه فوراً لصفحة الفهرس
//            return RedirectToPage("Index");
//        }
//    }
//}