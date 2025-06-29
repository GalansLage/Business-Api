namespace Application.DTOs
{
    public record ProductItemDto(
         int Id
        ,string productCode
        ,DateTime inTime
        ,string productSate
        ,ProductDto product
        );
    
    
}
