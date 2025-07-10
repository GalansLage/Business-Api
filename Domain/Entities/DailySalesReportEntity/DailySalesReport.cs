
using Domain.Entities.ProductTransactionEntity;

namespace Domain.Entities.DailySalesReportEntity
{
    public class DailySalesReport:Entity<int>
    {
        public DateOnly ReportDate { get; private set; }
        public int TotalTransactions { get; private set; }
        public int TotalItemsSold { get; private set; }
        public decimal TotalRevenue { get; private set; }
        public decimal TotalCost { get; private set; }
        public decimal TotalProfit { get; private set; }

        public DailySalesReport(DateOnly reportDate)
        {
            if(reportDate == default)
            {
                throw new ArgumentException("La fecha del informe no puede estar vacia.",nameof(reportDate));
            }

            ReportDate = reportDate;
            TotalTransactions = 0;
            TotalItemsSold = 0;
            TotalRevenue = 0;
            TotalCost = 0;
            TotalProfit = 0;

        }
        public void AddTransaction(ProductTransaction productTransaction)
        {
            if (!productTransaction.IsDeleted) return;

            this.TotalTransactions++;
            this.TotalItemsSold += productTransaction.Amount.AmountVO;

            decimal transactionRevenue = productTransaction.Products.Sum(item => item.Product.Price.PriceVO.Cash);
            decimal transactionCost = productTransaction.Products.Sum(item => item.Product.Cost.CostVO.Cash);

            this.TotalRevenue += transactionRevenue;
            this.TotalCost += transactionCost;
            this.TotalProfit = TotalRevenue - TotalCost;

        }

    }
}
