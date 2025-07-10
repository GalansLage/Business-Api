using Microsoft.EntityFrameworkCore.Storage;
using Domain.Repositories.ProductRepository;
using Domain.Repositories.ProductTransactionRepository;
using Domain.Repositories.ProviderRepository;
using Domain.Repositories.ProductItemRepository;
using Domain.Repositories.DailySalesReportRepository;

namespace Application.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        public IProductRepository ProductRepository { get; }
        public IProductItemRepository ProductItemRepository { get; }
        public IProductTransactionRepository ProductTransactionRepository { get; }
        public IProviderRepository ProviderRepository { get; }
        public IDailySalesReportRepository DailySalesReportRepository { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync(CancellationToken cancellationToken);
        Task<int> Save(CancellationToken cancellationToken);
        Task RollBackAsync(CancellationToken cancellationToken);
    }
}
