using Application.Core.Exceptions;
using Application.Features.Transactions.Commands.AddNewProducts;
using Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Commands.Transaction
{
    public class AddNewProductsCommandHandler : IRequestHandler<AddNewProductsCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddNewProductsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddNewProductsCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            var hasActiveTransaction = await _unitOfWork.ProductTransactionRepository.GetAll().AnyAsync(cancellationToken);

            if (!hasActiveTransaction)
            {
                await _unitOfWork.RollBackAsync(cancellationToken);
                throw new ConflictException("No hay una transacción activa");
            }
            var transaction = await _unitOfWork.ProductTransactionRepository.GetAll().Include(t=>t.Products).FirstOrDefaultAsync();
            var allItemsToProcess = new List<Domain.Entities.ProductItemEntity.ProductItem>();

            foreach (var productItem in request.products)
            {
                var availableItems = await _unitOfWork.ProductItemRepository
                    .FilterByIdAndState(productItem.ProductId, productItem.Quantity, cancellationToken);

                if (availableItems.Count < productItem.Quantity)
                {
                    await _unitOfWork.RollBackAsync(cancellationToken);
                    throw new ConflictException($"Stock insuficiente para el producto ID {productItem.ProductId}." +
                        $" Se requieren {productItem.Quantity} pero solo hay {availableItems.Count} disponibles.");
                }

                allItemsToProcess.AddRange(availableItems);


            }

            foreach (var itemToTransaction in allItemsToProcess)
            {
                itemToTransaction.SateChange(Domain.Core.Enums.PState.OutStore);
                itemToTransaction.ProductTransactionId = transaction!.Id;
                _unitOfWork.ProductItemRepository.Update(itemToTransaction);
            }

            transaction!.Products.AddRange(allItemsToProcess);

            transaction.UpdateAmount();

            _unitOfWork.ProductTransactionRepository.Update(transaction);
            await _unitOfWork.CommitAsync(cancellationToken);

            return true;
        }
    }
}
