
using Application.DTOs;
using Domain.Entities.ProductItemEntity;

namespace Application.Mappers
{
    public static class ProductItemMapper
    {
        public static ProductItemDto ToDto(ProductItem productItem)
        {
            return new ProductItemDto(
                Id: productItem.Id,
                productCode: productItem.ProductCode.Code,
                inTime: productItem.InTime.InTimeVO,
                productSate: productItem.ProductState.State,
                product: ProductMapper.ToDto(productItem.Product)
                );
        }
    }
}
