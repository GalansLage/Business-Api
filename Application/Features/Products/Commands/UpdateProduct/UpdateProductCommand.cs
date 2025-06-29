
using Domain.Core.Enums;
using MediatR;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(
            int Id,
            string productName,
            decimal amountPrice,
            decimal amountCost,
            CurrencyEnum currencyEnum,
            int providerId,
            string category
        ) :IRequest<bool>;
    
    
}
