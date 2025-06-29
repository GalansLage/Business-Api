

using Application.Features.Providers.Commands.HardDeleteProvider;
using Application.UnitOfWork;
using MediatR;

namespace Application.Handlers.Commands.Provider
{
    public class HardDeleteProviderCommandHandler : IRequestHandler<HardDeleteProviderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HardDeleteProviderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(HardDeleteProviderCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ProviderRepository.HardDelete(request.Id);
            await _unitOfWork.Save(cancellationToken);
            return result;
        }
    }
}
