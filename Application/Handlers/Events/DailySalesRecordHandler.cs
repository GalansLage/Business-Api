
using Application.UnitOfWork;
using Domain.DomainEvents.ProductTransactionEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.Events
{
    public class DailySalesRecordHandler : INotificationHandler<TransactionConfirmed>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DailySalesRecordHandler> _logger;

        public DailySalesRecordHandler(IUnitOfWork unitOfWork, ILogger<DailySalesRecordHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(TransactionConfirmed notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--- Nuevo Evento: Transacción Confirmada Recibida ---");

            var transaction = notification.ConfirmedTransaction;

            DateOnly reportDate = DateOnly.FromDateTime(transaction.DeletedTimeUtc);

            var dailySales = await _unitOfWork.DailySalesReportRepository.GetByDate(reportDate);

            if (dailySales == null)
            {
                dailySales = new Domain.Entities.DailySalesReportEntity.DailySalesReport(reportDate);
                await _unitOfWork.DailySalesReportRepository.Insert(dailySales);
            }

            dailySales.AddTransaction(transaction);

            await _unitOfWork.Save(cancellationToken);

            _logger.LogInformation($"Informe de ventas para {reportDate:yyyy-MM-dd} actualizado con la transacción ID {transaction.Id}.");

        }
    }
}
