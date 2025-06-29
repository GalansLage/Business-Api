
using Application.Core.Exceptions;
using Application.Features.Providers.Commands.UpdateProvider;
using Application.UnitOfWork;
using MediatR;

namespace Application.Handlers.Commands.Provider
{
    public class UpdateProviderCommandHandler : IRequestHandler<UpdateProviderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProviderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateProviderCommand request, CancellationToken cancellationToken)
        {
            var provider = await _unitOfWork.ProviderRepository.GetById(request.Id);
            if (provider == null) throw new NotFoundException("No se encontro un provedor con ese Id");

            var result = provider.Update(request.providerName, request.providerLastName, request.cI, request.number);
            _unitOfWork.ProviderRepository.Update(provider);
            await _unitOfWork.Save(cancellationToken);

            return result;

        }
    }
}
