using Application.Core;
using Application.Core.Exceptions;
using Application.DTOs;
using Application.Features.Providers.Queries.GetAllProviders;
using Application.Mappers;
using Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Queries.Provider
{
    public class GetAllProvidersQueryHandler : IRequestHandler<GetAllProvidersQuery, PagedResponse<ProviderWithStockDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllProvidersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResponse<ProviderWithStockDto>> Handle(GetAllProvidersQuery request, CancellationToken cancellationToken)
        {
            var totalRecordes = await _unitOfWork.ProviderRepository.GetAll().CountAsync();

            var providers = await _unitOfWork.ProviderRepository.GetAll().Include(p => p.Products)
                .OrderBy(p => p.Id).Skip((request.pageNumber - 1) * request.pageSize).Take(request.pageSize)
                .ToListAsync();

            if (providers == null) throw new NotFoundException("No hay provedores");

            foreach (var item in providers)
            {
                Console.WriteLine($"{item.Products.Count}");
            }

            var providerResponse = new List<ProviderWithStockDto>();

          

            foreach (var provider in providers)
            {
                var productWithStock = new List<ProductWithStockDto>();

                if (provider.Products != null)
                {
                    foreach (var item in provider.Products)
                    {
                        var stock = _unitOfWork.ProductItemRepository.GetAll().Where(pi => pi.ProductId == item.Id).Count();
                        productWithStock.Add(ProductMapper.ToDto(item, stock));
                    }
                }
                providerResponse.Add(ProviderMapper.ToDto(provider, productWithStock));
            }

            var pagedResponse = new PagedResponse<ProviderWithStockDto>(
                    providerResponse,
                    request.pageNumber,
                    request.pageSize,
                    totalRecordes
                );

            return pagedResponse;
        }
    }
}
