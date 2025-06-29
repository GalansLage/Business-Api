
using Application.Core.Exceptions;
using Application.Features.ProductItems.Commands.DeleteProductItem.SoftDelete;
using Application.UnitOfWork;
using MediatR;

namespace Application.Handlers.Commands.ProductItem
{
    public class SoftDeleteProductItemCommandHandler : IRequestHandler<SoftDeleteProductItemCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SoftDeleteProductItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(SoftDeleteProductItemCommand request, CancellationToken cancellationToken)
        {
            var productItem = await _unitOfWork.ProductItemRepository.GetById(request.Id);
            if (productItem == null) throw new NotFoundException("No existe un item con ese Id.");

            await _unitOfWork.ProductItemRepository.SoftDelete(request.Id);
            await _unitOfWork.Save(cancellationToken);
            return true;
        }
    }
}
