

using Application.DTOs;
using Application.Features.Providers.Commands.CreateProvider;
using Application.Mappers;
using Application.UnitOfWork;
using Domain.Entities.ProviderEntity;
using MediatR;

namespace Application.Handlers.Commands.Provider
{
    public class CreateProviderCommandHandler : IRequestHandler<CreateProviderCommand, ProviderDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProviderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProviderDto> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
        {
            var provider = new Domain.Entities.ProviderEntity.Provider(
                providerName: request.providerName,
                providerLastName: request.providerLastName,
                number: request.number,
                cI: request.cI,
                products: new List<Domain.Entities.ProductEntity.Product>()
                );

            await _unitOfWork.ProviderRepository.Insert(provider);
            await _unitOfWork.Save(cancellationToken);

            return ProviderMapper.ToDto(provider);

        }
    }
}
