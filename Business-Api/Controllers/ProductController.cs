using Application.Core;
using Application.DTOs;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.CreateProduct.CreateProductWithProvider;
using Application.Features.Products.Commands.DeleteProduct.HardDelete;
using Application.Features.Products.Commands.DeleteProduct.SoftDelete;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries;
using Application.Features.Products.Queries.GetProductById;
using Business_Api.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Business_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ISender _mediator;
        public ProductController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductWithStockDto>>> GetAllProducts([FromQuery] GetAllProductsQuery query)
            =>Ok(ApiResponse<PagedResponse<ProductWithStockDto>>.Success(await _mediator.Send(query)));


        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int Id)
        {
             var query = new GetProductByIdQuery(Id);
             return Ok(ApiResponse<ProductDto>.Success(await _mediator.Send(query)));
        }


        [HttpPost("InsertProductWithProvider")]
        public async Task<ActionResult<ProductDto>> CreateProductWithProvider([FromBody] CreateProductWithProviderCommand command)
        {
            var product = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { product.Id }, product);
        }

        [HttpPost("InsertProduct")]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductCommand command)
        {
            var product = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { product.Id }, product);
        }

        [HttpDelete("HardDelete/{Id}")]
        public async Task<ActionResult<bool>> HardDelete(int Id)
        {
            var command = new HardDeleteProductCommand(Id);
            return Ok(ApiResponse<bool>.Success(await _mediator.Send(command)));
        }

        [HttpDelete("SoftDelete/{Id}")]
        public async Task<ActionResult<bool>> SoftDelete(int Id)
        {
            var command = new SoftDeleteProductCommand(Id);
            return Ok(ApiResponse<bool>.Success(await _mediator.Send(command)));
        }
        [HttpPut("Update/{Id}")]
        public async Task<ActionResult<bool>> Update([FromRoute]int Id, [FromBody]ProductUpdateDto updateDto)
        {
            var command = new UpdateProductCommand(
                    Id,updateDto.productName,updateDto.amountPrice,updateDto.amountCost,updateDto.currencyEnum,updateDto.providerId,updateDto.category
                );

            return Ok(ApiResponse<bool>.Success(await _mediator.Send(command)));
        }
        
    }
}
