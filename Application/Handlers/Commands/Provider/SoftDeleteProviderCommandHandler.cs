using Application.Features.Providers.Commands.SoftDelete;
using Application.UnitOfWork;
using MediatR;

namespace Application.Handlers.Commands.Provider
{
    public class SoftDeleteProviderCommandHandler : IRequestHandler<SoftDeleteProviderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SoftDeleteProviderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(SoftDeleteProviderCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ProviderRepository.SoftDelete(request.Id);
            await _unitOfWork.Save(cancellationToken);
            return result;
        }
    }
}
