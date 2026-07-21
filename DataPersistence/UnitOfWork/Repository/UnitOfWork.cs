using UnitOfWork.Data;
using UnitOfWork.Interface;

namespace UnitOfWork.Repository
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private IProductRepository _productRepository = null!;
        
        public IProductRepository repository => _productRepository ??= new EFProductRepository(context);
        
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
          =>  context.SaveChangesAsync(cancellationToken);
        
        public void Dispose() =>   context.Dispose();

    }
}
