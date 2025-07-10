
using Application.DTOs;
using Domain.Entities.ProductEntity;

namespace Application.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(Product domain)
        {
            return new ProductDto(
                Id: domain.Id,
                ProductName: domain.ProductName.Name,
                Price: domain.Price.PriceVO.Cash,
                Cost: domain.Cost.CostVO.Cash,
                Currency: domain.Cost.CostVO.CurrencyVO.CurrencyVO,
                ProviderName: domain.Provider.ProviderName.Name,
                Category: domain.Category.CategoryVO
                );
        }
        public static ProductWithStockDto ToDto(Product domain,int stock)
        {
            return new ProductWithStockDto(
                Id: domain.Id,
                ProductName: domain.ProductName.Name,
                Price: domain.Price.PriceVO.Cash,
                Cost: domain.Cost.CostVO.Cash,
                Currency: domain.Cost.CostVO.CurrencyVO.CurrencyVO,
                ProviderName: domain.Provider.ProviderName.Name,
                Category: domain.Category.CategoryVO,
                stockCount:stock
                );
        }
    }
}
