using Application.Core.Exceptions;
using Application.Features.ProductItems.Commands.CreateProductItem;
using Application.UnitOfWork;
using MediatR;

namespace Application.Handlers.Commands.ProductItem
{
    public class CreateProductItemCommandHandler : IRequestHandler<CreateProductItemCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CreateProductItemCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetById(request.productId);
            if (product == null) throw new NotFoundException("No existe un producto con ese Id");

            var newItems = new List<Domain.Entities.ProductItemEntity.ProductItem>();

            for(int i = 0; i < request.itemsAmount; i++)
            {
                var uniqueCode = $"{request.productCode}-{Guid.NewGuid().ToString().Substring(0, 8)}";
                var newItem = new Domain.Entities.ProductItemEntity.ProductItem(
                        productCode:uniqueCode,
                        product: product
                    );
                newItems.Add(newItem);
            }

            await _unitOfWork.ProductItemRepository.InsetProductsItems(newItems);
            await _unitOfWork.Save(cancellationToken);

            return $"{request.itemsAmount} items del producto {product.ProductName.Name} han sido agregados al inventario.";
        }
    }
}
