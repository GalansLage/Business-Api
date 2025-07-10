
using FluentValidation;

namespace Application.Features.DailySalesReport.Queries.GetAllDailySalesReport
{
    public class GetAllDailySalesReportValidator:AbstractValidator<GetAllDailySalesReportQuery>
    {
        public GetAllDailySalesReportValidator()
        {
            RuleFor(x => x.pageNumber).GreaterThan(0).WithMessage("El número de página debe ser mayor que 0.");

            RuleFor(x => x.pageSize)
                .GreaterThan(0)
                .WithMessage("El tamaño de página debe ser mayor que 0.")
                .LessThanOrEqualTo(100)
                .WithMessage("El tamaño de página no puede ser mayor que 100.");
        }
    }
}
