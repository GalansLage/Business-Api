

using Domain.Entities.ProductTransactionEntity;
using Domain.Repositories.ProductTransactionRepository;

namespace Infrastructure.Persistence.Repositories.ProductTransactionRepository
{
    public class ProductTransactionRepository : GenericRepository<ProductTransaction, int>, IProductTransactionRepository
    {
        public ProductTransactionRepository(BusinessContext context) : base(context)
        {
        }
    }
}
