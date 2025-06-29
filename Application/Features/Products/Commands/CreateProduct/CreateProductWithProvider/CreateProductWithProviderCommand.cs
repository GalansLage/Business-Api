using Application.DTOs;
using Domain.Core.Enums;
using MediatR;

namespace Application.Features.Products.Commands.CreateProduct.CreateProductWithProvider
{
    public record CreateProductWithProviderCommand(
            string productName,
            decimal amountPrice,
            CurrencyEnum currencyEnum,
            decimal amountCost,
            string category,
            string providerName, string providerLastName, string cI, string number
        ):IRequest<ProductDto>;
    
}
