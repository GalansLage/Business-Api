
using Application.Features.Products.Commands.DeleteProduct.HardDelete;
using Application.UnitOfWork;
using MediatR;

namespace Application.Handlers.Commands.Product
{
    public class HardDeleteProductCommandHandler : IRequestHandler<HardDeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public HardDeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(HardDeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result =  await _unitOfWork.ProductRepository.HardDelete(request.Id);
            await _unitOfWork.Save(cancellationToken);
            return result;
        }
    }
}
