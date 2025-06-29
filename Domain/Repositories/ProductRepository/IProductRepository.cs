using Domain.Entities.ProductEntity;

namespace Domain.Repositories.ProductRepository
{
    public interface IProductRepository:IGenericRepository<Product,int>
    {
        Task<Product> GetProductByIdWithProvider(int Id);
        Task<Product> FindByName(string name);
    }
}
