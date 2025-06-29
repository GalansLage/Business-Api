

using Application.Core.Exceptions;
using Application.Features.Transactions.Commands.DeleteProducts;
using Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Commands.Transaction
{
    public class DeleteProductsCommandHandler : IRequestHandler<DeleteProductsCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProductsCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            var hasActiveTransaction = await _unitOfWork.ProductTransactionRepository.GetAll().AnyAsync(cancellationToken);

            if (!hasActiveTransaction)
            {
                await _unitOfWork.RollBackAsync(cancellationToken);
                throw new ConflictException("No hay una transacción activa");
            }

            var transaction = await _unitOfWork.ProductTransactionRepository.GetAll().Include(t => t.Products).FirstOrDefaultAsync(cancellationToken);
            var itemsIdsToRemove = request.ItemsToRemove.Select(i => i.ProductItemId).ToHashSet();

            var itemsToProcess = transaction!.Products
                .Where(p => itemsIdsToRemove.Contains(p.Id)).ToList();

            if(itemsToProcess.Count != itemsIdsToRemove.Count)
            {
                await _unitOfWork.RollBackAsync(cancellationToken);
                throw new ConflictException("Se intentó eliminar uno o más ítems que no pertenecen a la transacción activa.");
            }

            foreach (var item in itemsToProcess)
            {
                item.SateChange(Domain.Core.Enums.PState.InStore);
                item.ProductTransactionId = null;
                transaction.Products.Remove(item);
            }

            transaction.UpdateAmount();

            await _unitOfWork.CommitAsync(cancellationToken);

            return true;
        }
    }
}
