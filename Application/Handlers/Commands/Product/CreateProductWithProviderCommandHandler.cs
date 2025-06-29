using Application.DTOs;
using Application.Features.Products.Commands.CreateProduct.CreateProductWithProvider;
using Application.Mappers;
using Application.UnitOfWork;
using Domain.Entities.ProviderEntity;
using MediatR;

namespace Application.Handlers.Commands.Product
{
    public class CreateProductWithProviderCommandHandler : IRequestHandler<CreateProductWithProviderCommand, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductWithProviderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(CreateProductWithProviderCommand request, CancellationToken cancellationToken)
        {
            var sameName = await _unitOfWork.ProductRepository.FindByName(request.productName);
            if (sameName != null) throw new InvalidOperationException("Ya existe un producto con ese nombre");

            await _unitOfWork.BeginTransactionAsync();

            var provider = new Domain.Entities.ProviderEntity.Provider(providerName: request.providerName, providerLastName: request.providerLastName,
                cI: request.cI, number: request.number, products: new());

            await _unitOfWork.ProviderRepository.Insert(provider);

            var product = new Domain.Entities.ProductEntity.Product(productName: request.productName
                , cost: new Domain.ValueObjects.MoneyValueObjects.Money(request.amountCost, request.currencyEnum)
                , price: new Domain.ValueObjects.MoneyValueObjects.Money(request.amountPrice, request.currencyEnum)
                , providerId: provider.Id,category:request.category);

            provider.Products.Add(product);

            await _unitOfWork.CommitAsync(cancellationToken);

            return ProductMapper.ToDto(product);
        }
    }
}

