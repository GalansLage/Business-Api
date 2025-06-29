
using Application.Core.Exceptions;
using Application.Features.Transactions.Commands.CancelTransaction;
using Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Commands.Transaction
{
    public class CancelTransactionCommandHandler : IRequestHandler<CancelTransactionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CancelTransactionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CancelTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _unitOfWork.ProductTransactionRepository.GetAll().Include(t=>t.Products).FirstAsync();
            if (transaction == null) throw new NotFoundException("No existe ninguna transacción activa. ");

            foreach (var item in transaction.Products)
            {
                item.SateChange(Domain.Core.Enums.PState.InStore);
                item.ProductTransactionId = null;
                _unitOfWork.ProductItemRepository.Update(item);
            }

            await _unitOfWork.ProductTransactionRepository.HardDelete(transaction.Id);
            await _unitOfWork.Save(cancellationToken);
            return true;
        }
    }
}
