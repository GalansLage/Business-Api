namespace Application.DTOs
{
    public record ProviderDto(
        int Id,
        string providerName,
        string providerLastName,
        string ci,
        string number,
        List<ProductDto> products
        );
}
