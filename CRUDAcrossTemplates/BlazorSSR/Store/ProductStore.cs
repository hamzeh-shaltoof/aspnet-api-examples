using BlazorSSR.Models;

namespace BlazorSSR.Store
{
    public  class ProductStore
    {
        private  readonly List<Product> _products = 
           [
           new () { Id = Guid.NewGuid() , Name = "Soda" , Price = 0.50m , Category="Food" , Description ="this is Soda"},
           new () { Id = Guid.NewGuid() , Name = "Ice-Cream" , Price = 0.75m , Category="Food" , Description ="this is Ice-Cream"}
           ]; 

        public  IEnumerable<Product> GetAll() => _products;
        public  Product? GetById(Guid id) => _products.FirstOrDefault(x => x.Id == id);
        public  void Add(Product product) => _products.Add(product);
        public  bool Update(Product product) 
        {
            var existing = GetById(product.Id);
            if (existing is null)
                return false;
            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Category = product.Category;
            existing.Description = product.Description;

            return true;
        }
        public  bool Delete(Product product) 
        {
            var existing = GetById(product.Id);
            return existing is null ? false : _products.Remove(product);
        }
    }
}
