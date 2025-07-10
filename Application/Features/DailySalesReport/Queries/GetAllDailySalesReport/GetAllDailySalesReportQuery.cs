
using Application.Core;
using Application.DTOs;
using MediatR;

namespace Application.Features.DailySalesReport.Queries.GetAllDailySalesReport
{
    public record GetAllDailySalesReportQuery(int pageNumber = 1,int pageSize = 10) : IRequest<PagedResponse<DailySalesReportDto>>;
    
}
