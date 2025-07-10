namespace Application.DTOs
{
    public record DailySalesReportDto
    (
         int Id,
         DateOnly ReportDate,
         int TotalTransactions,
         int TotalItemsSold,
         decimal TotalRevenue,
         decimal TotalCost,
         decimal TotalProfit
    );
}
