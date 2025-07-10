using Application.Core;
using Application.Core.Exceptions;
using Application.DTOs;
using Application.Features.Products.Queries;
using Application.Mappers;
using Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Queries.Product
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResponse<ProductWithStockDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResponse<ProductWithStockDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var totalRecords = _unitOfWork.ProductRepository.GetAll().Count();
            var products = await _unitOfWork.ProductRepository.GetAll().Include(p=>p.Provider)
                .OrderBy(p=>p.Id).Skip((request.PageNumber-1)*request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            if (products == null) throw new NotFoundException("No hay productos.");

            var productResponse = new List<ProductWithStockDto>();

            foreach (var product in products)
            {
                var stock = _unitOfWork.ProductItemRepository.GetAll().Where(p => p.ProductId == product.Id).Count();
                productResponse.Add(ProductMapper.ToDto(product,stock));
            }

            var pagedResponse = new PagedResponse<ProductWithStockDto>(
                productResponse,
                request.PageNumber,
                request.PageSize,
                totalRecords
                );

            return pagedResponse;
        }
    }
}
