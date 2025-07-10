

using Application.DTOs;
using Domain.Entities.DailySalesReportEntity;

namespace Application.Mappers
{
    public static class DailySalesReportMapper
    {
        public static DailySalesReportDto ToDto(DailySalesReport dailySalesReport)
        {
            return new DailySalesReportDto
            (
                Id:dailySalesReport.Id,
                ReportDate: dailySalesReport.ReportDate,
                TotalTransactions: dailySalesReport.TotalTransactions,
                TotalItemsSold: dailySalesReport.TotalItemsSold,
                TotalRevenue: dailySalesReport.TotalRevenue,
                TotalCost: dailySalesReport.TotalCost,
                TotalProfit: dailySalesReport.TotalProfit
            );

        }
    }
}
