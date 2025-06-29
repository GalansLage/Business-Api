using Domain.Core.Enums;

namespace Business_Api.Dtos
{
    public record ProductUpdateDto(
            string productName,
            decimal amountPrice,
            decimal amountCost,
            CurrencyEnum currencyEnum,
            int providerId,
            string category
        );
    
}
