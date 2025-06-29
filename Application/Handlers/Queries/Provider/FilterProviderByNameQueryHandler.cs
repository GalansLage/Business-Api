
using Application.Core;
using Application.DTOs;
using Application.Features.Providers.Queries.FilterByName;
using Application.Mappers;
using Application.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Queries.Provider
{
    public class FilterProviderByNameQueryHandler : IRequestHandler<FilterProviderByNameQuery, PagedResponse<ProviderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FilterProviderByNameQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResponse<ProviderDto>> Handle(FilterProviderByNameQuery request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.ProviderRepository.FilterByName(request.searchName);


            var providers = new List<ProviderDto>();

            var pagedData = await result.Include(pr=>pr.Products).Skip((request.pageNumber - 1) * request.pageSize)
                .Take(request.pageSize).ToListAsync();

            foreach (var provider in pagedData)
            {
                providers.Add(ProviderMapper.ToDto(provider));
            }

            var totalRecords = providers.Count();

            return new PagedResponse<ProviderDto>(
                 providers,
                 request.pageNumber,
                 request.pageSize, totalRecords
                );

        }
    }
}
