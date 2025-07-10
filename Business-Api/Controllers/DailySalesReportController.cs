using Application.Core;
using Application.DTOs;
using Application.Features.DailySalesReport.Queries.GetAllDailySalesReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Business_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DailySalesReportController:ControllerBase
    {
        private readonly ISender _mediator;

        public DailySalesReportController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<DailySalesReportDto>>> GetAllReports([FromQuery] GetAllDailySalesReportQuery query)
            => Ok(ApiResponse<PagedResponse<DailySalesReportDto>>.Success(await _mediator.Send(query)));
    }
}
