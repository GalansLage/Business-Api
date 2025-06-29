
using Application.Core.Exceptions;
using Application.Features.Transactions.Commands.StartTransaction;
using Application.UnitOfWork;
using Domain.Entities.ProductTransactionEntity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Commands.Transaction
{
    public class StartTransactionCommandHandler : IRequestHandler<StartTransactionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public StartTransactionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(StartTransactionCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            var hasActiveTransaction = await _unitOfWork.ProductTransactionRepository.GetAll().AnyAsync(cancellationToken);

            if (hasActiveTransaction)
            {
                await _unitOfWork.RollBackAsync(cancellationToken);
                throw new ConflictException("Ya hay una transacción activa");
            }

            var transaction = new ProductTransaction(new List<Domain.Entities.ProductItemEntity.ProductItem>());
            await _unitOfWork.ProductTransactionRepository.Insert(transaction);

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
                itemToTransaction.ProductTransactionId = transaction.Id;
                _unitOfWork.ProductItemRepository.Update(itemToTransaction);
            }

            transaction.Products.AddRange(allItemsToProcess);

            transaction.UpdateAmount();

            await _unitOfWork.CommitAsync(cancellationToken);

            return true;
        }
    }
}
