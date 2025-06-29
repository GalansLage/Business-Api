using Application.Core.Exceptions;
using Application.DTOs;
using Application.Features.ProductItems.Queries.GetProductItemById;
using Application.Mappers;
using Application.UnitOfWork;
using MediatR;

namespace Application.Handlers.Queries.ProductItem
{
    public class GetProductItemByIdQueryHandler : IRequestHandler<GetProductItemByIdQuery, ProductItemDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductItemByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductItemDto> Handle(GetProductItemByIdQuery request, CancellationToken cancellationToken)
        {
            var productItem = await _unitOfWork.ProductItemRepository.GetProductItemById(request.Id);
            if (productItem == null) throw new NotFoundException("No se encontro ningun item con ese Id");

            return ProductItemMapper.ToDto(productItem);
        }
    }
}
