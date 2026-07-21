namespace UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductRepository repository { get; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
