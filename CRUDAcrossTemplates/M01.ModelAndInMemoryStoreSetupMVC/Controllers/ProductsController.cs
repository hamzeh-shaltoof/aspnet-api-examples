using M01.ModelAndInMemoryStoreSetupMVC.Model;
using M01.ModelAndInMemoryStoreSetupMVC.Store;
using Microsoft.AspNetCore.Mvc;

namespace M01.ModelAndInMemoryStoreSetupMVC.Controllers
{
    public class ProductsController(ProductStore store) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var products = store.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var product = store.GetById(id);
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            product.Id = Guid.NewGuid();
            store.Add(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var product = store.GetById(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            store.Update(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet , ActionName("Delete")]
        public IActionResult  Delete(Guid id)
        {
            var product = store.GetById(id);

             store.Delete(product);
            return RedirectToAction(nameof(Index));

        }

    }
}
