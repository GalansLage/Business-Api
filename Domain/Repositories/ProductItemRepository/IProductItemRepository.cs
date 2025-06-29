

using Domain.Entities.ProductItemEntity;

namespace Domain.Repositories.ProductItemRepository
{
    public interface IProductItemRepository:IGenericRepository<ProductItem,int>
    {
        Task<bool> InsetProductsItems(List<ProductItem> productItems);
        Task<ProductItem?> GetProductItemById(int Id);

        Task<List<ProductItem>> FilterByIdAndState(int Id,int quantity,CancellationToken cancellationToken);
    }
}
