namespace Application.DTOs
{
    public record ProviderWithStockDto(
        int Id,
        string providerName,
        string providerLastName,
        string ci,
        string number,
        List<ProductWithStockDto> products
        );

    public record ProviderDto(
        int Id,
        string providerName,
        string providerLastName,
        string ci,
        string number,
        List<ProductDto> products
        );
}
