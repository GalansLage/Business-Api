

using Application.Core.Exceptions;
using Application.Features.Transactions.Commands.ConfirmTransaction;
using Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Commands.Transaction
{
    public class ConfirmTransactionCommandHandler : IRequestHandler<ConfirmTransactionCommand, decimal>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmTransactionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            return inCome;
        }
    }
}
