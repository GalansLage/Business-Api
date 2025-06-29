

using Application.Features.Products.Commands.DeleteProduct.SoftDelete;
using Application.UnitOfWork;
using MediatR;

namespace Application.Handlers.Commands.Product
{
    public class SoftDeleteProductCommandHandler : IRequestHandler<SoftDeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SoftDeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(SoftDeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ProductRepository.SoftDelete(request.Id);
            await _unitOfWork.Save(cancellationToken);
            return result;
        }
    }
}
