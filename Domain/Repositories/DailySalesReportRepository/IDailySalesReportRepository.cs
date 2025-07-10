

using Domain.Entities.DailySalesReportEntity;

namespace Domain.Repositories.DailySalesReportRepository
{
    public interface IDailySalesReportRepository:IGenericRepository<DailySalesReport,int>
    {
        Task<DailySalesReport?> GetByDate(DateOnly date);
    }
}
