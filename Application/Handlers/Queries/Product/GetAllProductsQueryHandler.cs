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
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResponse<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResponse<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var totalRecords = _unitOfWork.ProductRepository.GetAll().Count();
            var products = await _unitOfWork.ProductRepository.GetAll().Include(p=>p.Provider)
                .OrderBy(p=>p.Id).Skip((request.PageNumber-1)*request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            if (products == null) throw new NotFoundException("No hay productos.");

            var productResponse = new List<ProductDto>();

            foreach (var product in products)
            {
                productResponse.Add(ProductMapper.ToDto(product));
            }

            var pagedResponse = new PagedResponse<ProductDto>(
                productResponse,
                request.PageNumber,
                request.PageSize,
                totalRecords
                );

            return pagedResponse;
        }
    }
}
