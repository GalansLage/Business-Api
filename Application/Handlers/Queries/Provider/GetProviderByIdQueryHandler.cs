
using Application.Core.Exceptions;
using Application.DTOs;
using Application.Features.Providers.Queries.GetProviderById;
using Application.Mappers;
using Application.UnitOfWork;
using MediatR;

namespace Application.Handlers.Queries.Provider
{
    public class GetProviderByIdQueryHandler : IRequestHandler<GetProviderByIdQuery, ProviderDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProviderByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ProviderDto> Handle(GetProviderByIdQuery request, CancellationToken cancellationToken)
        {
            var provider = await _unitOfWork.ProviderRepository.GetProviderWithProduct(request.Id);

            return ProviderMapper.ToDto(provider ?? throw new NotFoundException("No se encontro un provedor con ese Id"));
        }
    }
}
