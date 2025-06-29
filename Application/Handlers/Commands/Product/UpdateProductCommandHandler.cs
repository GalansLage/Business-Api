using Application.Core.Exceptions;
using Application.Features.Products.Commands.UpdateProduct;
using Application.UnitOfWork;
using MediatR;

namespace Application.Handlers.Commands.Product
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetById(request.Id);
            if (product == null) throw new NotFoundException("No existe un producto con ese Id");

            var provider = await _unitOfWork.ProviderRepository.GetById(request.providerId);
            if (provider == null) throw new NotFoundException("No se encontro un provedor con ese Id");

            var result = product.Update(
                    request.productName,
                    request.amountPrice,
                    request.amountCost,
                    request.currencyEnum,
                    provider,
                    request.category
                );
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.Save(cancellationToken);
            return result;
        }
    }
}
