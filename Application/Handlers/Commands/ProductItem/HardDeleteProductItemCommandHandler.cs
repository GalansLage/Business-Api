using Application.Core.Exceptions;
using Application.Features.ProductItems.Commands.DeleteProductItem.HardDelete;
using Application.UnitOfWork;
using MediatR;

namespace Application.Handlers.Commands.ProductItem
{
    public class HardDeleteProductItemCommandHandler : IRequestHandler<HardDeleteProductItemCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HardDeleteProductItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(HardDeleteProductItemCommand request, CancellationToken cancellationToken)
        {
            var productItem = await _unitOfWork.ProductItemRepository.GetById(request.Id);
            if (productItem == null) throw new NotFoundException("No existe un item con ese Id.");

            await _unitOfWork.ProductItemRepository.HardDelete(request.Id);
            await _unitOfWork.Save(cancellationToken);

            return true;
        }
    }
}
