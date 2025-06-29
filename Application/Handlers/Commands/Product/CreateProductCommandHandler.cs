
using Application.Core.Exceptions;
using Application.DTOs;
using Application.Features.Products.Commands.CreateProduct;
using Application.Mappers;
using Application.UnitOfWork;
using Domain.Entities.ProviderEntity;
using MediatR;

namespace Application.Handlers.Commands.Product
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var sameName = await _unitOfWork.ProductRepository.FindByName(request.productName);
            if (sameName != null) throw new InvalidOperationException("Ya existe un producto con ese nombre");

            var provider =await _unitOfWork.ProviderRepository.GetById(request.providerId);
            if (provider == null) throw new NotFoundException("No existe ningun proveedor con ese Id");

            var product = new Domain.Entities.ProductEntity.Product(productName: request.productName
                , cost: new Domain.ValueObjects.MoneyValueObjects.Money(request.amountCost, request.currencyEnum)
                , price: new Domain.ValueObjects.MoneyValueObjects.Money(request.amountPrice, request.currencyEnum)
                , providerId: request.providerId, category: request.category);

            await _unitOfWork.ProductRepository.Insert(product);
            await _unitOfWork.Save(cancellationToken);

            return ProductMapper.ToDto(product);
        }
    }
}
