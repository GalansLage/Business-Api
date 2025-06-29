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
    public class GetAllProvidersQueryHandler : IRequestHandler<GetAllProvidersQuery, PagedResponse<ProviderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllProvidersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResponse<ProviderDto>> Handle(GetAllProvidersQuery request, CancellationToken cancellationToken)
        {
            var totalRecordes = _unitOfWork.ProviderRepository.GetAll().Count();

            var providers = await _unitOfWork.ProviderRepository.GetAll().Include(p => p.Products)
                .OrderBy(p => p.Id).Skip((request.pageNumber - 1) * request.pageSize).Take(request.pageSize)
                .ToListAsync();

            if (providers == null) throw new NotFoundException("No hay provedores");

            var providerResponse = new List<ProviderDto>();

            foreach(var provider in providers)
            {
                providerResponse.Add(ProviderMapper.ToDto(provider));
            }

            var pagedResponse = new PagedResponse<ProviderDto>(
                    providerResponse,
                    request.pageNumber,
                    request.pageSize,
                    totalRecordes
                );

            return pagedResponse;
        }
    }
}
