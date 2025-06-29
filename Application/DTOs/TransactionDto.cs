namespace Application.DTOs
{
    public record TransactionDto(
        int quantity,DateTime startTime,List<ProductItemDto> products,decimal TotalIncome
        );
    
}
