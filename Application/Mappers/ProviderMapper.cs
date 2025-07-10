using Application.DTOs;
using Domain.Entities.ProductEntity;
using Domain.Entities.ProviderEntity;

namespace Application.Mappers
{
    public static class ProviderMapper
    {
        public static ProviderDto ToDto(Provider provider)
        {
            return new ProviderDto(
                Id: provider.Id,
                providerName: provider.ProviderName.Name,
                providerLastName: provider.ProviderLastName.Name,
                ci: provider.CI.CIVO,
                number: provider.Number.NumberVO,
                products:GetProducts(provider.Products)
                );
        }
        public static ProviderWithStockDto ToDto(Provider provider, List<ProductWithStockDto> stock)
        {
            Console.WriteLine($"{stock.Count}");
            return new ProviderWithStockDto(
                Id:provider.Id,
                providerName: provider.ProviderName.Name,
                providerLastName: provider.ProviderLastName.Name,
                ci: provider.CI.CIVO,
                number:provider.Number.NumberVO,
                products:stock
                );
        }

        public static List<ProductDto> GetProducts(List<Product> products)
        {
            var newProducts = new List<ProductDto>();

            if (products != null)
            {
                foreach (var product in products)
                {
                    newProducts.Add(ProductMapper.ToDto(product));
                }
            }
          
            return newProducts;
        }
    }
}
