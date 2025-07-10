
using Domain.Entities.DailySalesReportEntity;
using Domain.Repositories.DailySalesReportRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.DailySalesReportRepository
{
    public class DailySalesReportRepository : GenericRepository<DailySalesReport, int>, IDailySalesReportRepository
    {
        public  DailySalesReportRepository(BusinessContext context) : base(context)
        {
        }

        public async Task<DailySalesReport?> GetByDate(DateOnly date)
            => await _context.DailySalesReports.FirstOrDefaultAsync(ds => ds.ReportDate.Equals(date));

        
    }
}
