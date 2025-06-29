
namespace Application.DTOs
{
    public record ProductDto(
        int Id,
        string ProductName,
        decimal Price,
        decimal Cost,
        string Currency,
        string ProviderName,
        string Category
        );
    
}
