

using Application.Core.Exceptions;
using Application.Features.Transactions.Commands.ConfirmTransaction;
using Application.UnitOfWork;
using Domain.DomainEvents.ProductTransactionEvents;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Commands.Transaction
{
    public class ConfirmTransactionCommandHandler : IRequestHandler<ConfirmTransactionCommand, decimal>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisher _publisher;

        public ConfirmTransactionCommandHandler(IUnitOfWork unitOfWork, IPublisher publisher)
        {
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<decimal> Handle(ConfirmTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _unitOfWork.ProductTransactionRepository.GetAll().Include(t => t.Products).ThenInclude(p=>p.Product).FirstAsync();
            if (transaction == null) throw new NotFoundException("No hay transacciones activas. ");

            foreach (var item in transaction.Products)
            {
                item.IsDeletedChange();
                item.SateChange(Domain.Core.Enums.PState.Sell);
                _unitOfWork.ProductItemRepository.Update(item);
            }

            var inCome = transaction.ConfirmTransaction();
            _unitOfWork.ProductTransactionRepository.Update(transaction);
            await _unitOfWork.Save(cancellationToken);

            var transactionEvent = new TransactionConfirmed(transaction);
            await _publisher.Publish(transactionEvent, cancellationToken);

            return inCome;
        }
    }
}
