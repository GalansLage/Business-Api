

namespace Application.DTOs
{
    public record ProductWithStockDto(
        int Id,
        string ProductName,
        decimal Price,
        decimal Cost,
        string Currency,
        string ProviderName,
        string Category,
        int stockCount
        );


}
