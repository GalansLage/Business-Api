
using Application.Core;
using Application.Core.Exceptions;
using Application.DTOs;
using Application.Features.ProductItems.Queries.GetAllProductsItems;
using Application.Mappers;
using Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Queries.ProductItem
{
    public class GetAllProductItemQueryHandler : IRequestHandler<GetAllProductItemQuery, PagedResponse<ProductItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductItemQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResponse<ProductItemDto>> Handle(GetAllProductItemQuery request, CancellationToken cancellationToken)
        {
            var totalRecords = _unitOfWork.ProductItemRepository.GetAll().Count();

            var productsItems = await _unitOfWork.ProductItemRepository.GetAll().Include(pi=>pi.Product).ThenInclude(p=>p.Provider)
                .OrderBy(pi=>pi.Id).Skip((request.PageNumber-1)*request.PageSize).Take(request.PageSize).ToListAsync();

            if (productsItems == null) throw new NotFoundException("No hay productos en el inventario.");

            var productItemResponse = new List<ProductItemDto>();

            foreach(var product in productsItems)
            {
                productItemResponse.Add(ProductItemMapper.ToDto(product));
            }

            var pagedResponse = new PagedResponse<ProductItemDto>(
                productItemResponse,
                request.PageNumber,
                request.PageSize,
                totalRecords
                );

            return pagedResponse;
        }
    }
}
