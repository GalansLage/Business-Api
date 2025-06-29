
using Application.DTOs;
using Domain.Entities.ProductTransactionEntity;

namespace Application.Mappers
{
    public static class TransactionMapper
    {
        public static TransactionDto ToDto(ProductTransaction product)
        {
            var productsDto = new List<ProductItemDto>();

            foreach (var productItem in product.Products)
            {
                productsDto.Add(ProductItemMapper.ToDto(productItem));
            }

            return new TransactionDto(
                quantity: product.Amount.AmountVO,
                startTime: product.Date.InTimeVO,
                products: productsDto,
                TotalIncome:product.TotalIncome()
                );
        }
    }
}
