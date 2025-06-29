using Domain.Core.Enums;

namespace Business_Api.Models
{
    public record CreateProductRequest
    {
        public required string ProductName { get; init; }
        public required string ProductCode { get; init; }
        public required decimal Price { get; init; }
        public required decimal Cost { get; init; }
        public required CurrencyEnum Currency { get; init; }
        public required int ProviderId { get; init; }
        public required int StockStore { get; init; }
        public required string Category { get; init; }

    }
}
