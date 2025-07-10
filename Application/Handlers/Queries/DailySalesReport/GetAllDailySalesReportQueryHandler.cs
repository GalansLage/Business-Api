using Application.Core;
using Application.Core.Exceptions;
using Application.DTOs;
using Application.Features.DailySalesReport.Queries.GetAllDailySalesReport;
using Application.Mappers;
using Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Queries.DailySalesReport
{
    public class GetAllDailySalesReportQueryHandler : IRequestHandler<GetAllDailySalesReportQuery, PagedResponse<DailySalesReportDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllDailySalesReportQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PagedResponse<DailySalesReportDto>> Handle(GetAllDailySalesReportQuery request, CancellationToken cancellationToken)
        {
            var totalRecords = _unitOfWork.DailySalesReportRepository.GetAll().Count();

            var dailySales = await _unitOfWork.DailySalesReportRepository.GetAll()
               .OrderBy(pi => pi.Id).Skip((request.pageNumber - 1) * request.pageSize).Take(request.pageSize).ToListAsync();

            if (dailySales == null) throw new NotFoundException("No hay Reportes actualmente.");

            var newDailySales = new List<DailySalesReportDto>();

            foreach (var report in dailySales)
            {
                newDailySales.Add(DailySalesReportMapper.ToDto(report));
            }

            var pagedResponse = new PagedResponse<DailySalesReportDto>(
              newDailySales,
              request.pageNumber,
              request.pageSize,
              totalRecords
              );

            return pagedResponse;
        }
    }
}
