
using Application.DTOs;
using Domain.Core.Enums;
using MediatR;

namespace Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(
            string productName,
            decimal amountPrice,
            decimal amountCost,
            CurrencyEnum currencyEnum,
            int providerId,
            string category
        ) : IRequest<ProductDto>;
    
}
