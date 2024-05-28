using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIwithCQRS_Dapper_EF.CQRS.Commands.CreateProductCommand;
using MinimalAPIwithCQRS_Dapper_EF.CQRS.Commands.DeleteProductCommand;
using MinimalAPIwithCQRS_Dapper_EF.CQRS.Commands.UpdateProductCommand;
using MinimalAPIwithCQRS_Dapper_EF.CQRS.Queries.GetProducts;

namespace MinimalAPIwithCQRS_Dapper_EF
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ProductsController : ControllerBase
    {

        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var query = new GetProductsQuery();

            var products = await _mediator.Send(query, cancellationToken);
            
            return Ok(products);
        }
        
        [HttpPost("create")]
        public async Task<ActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("update")]
        public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        /*
        
  { "product": {
    "productName": "test",
    "supplierId": 1,
    "categoryId": 1,
    "quantityPerUnit": "string",
    "unitPrice": 0,
    "unitsInStock": 0,
    "unitsOnOrder": 0,
    "reorderLevel": 0,
    "discontinued": true
    }
  }
*/



    }
}