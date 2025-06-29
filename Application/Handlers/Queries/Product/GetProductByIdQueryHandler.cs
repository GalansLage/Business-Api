using Application.Core.Exceptions;
using Application.DTOs;
using Application.Features.Products.Queries.GetProductById;
using Application.Mappers;
using Application.UnitOfWork;
using MediatR;

namespace Application.Handlers.Queries.Product
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {

            return ProductMapper.ToDto(await _unitOfWork.ProductRepository.GetProductByIdWithProvider(request.Id)
                ?? throw new NotFoundException("No se encontró ningun producto con ese valor."));
        }
    }
}
