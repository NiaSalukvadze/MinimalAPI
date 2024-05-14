using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIwithCQRS_EF_Dapper.CQRS.Commands.CreateProductCommand;
using MinimalAPIwithCQRS_EF_Dapper.CQRS.Commands.DeleteProductCommand;
using MinimalAPIwithCQRS_EF_Dapper.CQRS.Commands.UpdateProductCommand;
using MinimalAPIwithCQRS_EF_Dapper.CQRS.Queries.GetProducts;
using MinimalAPIwithCQRS_EF_Dapper.Models;

namespace MinimalAPIwithCQRS_EF_Dapper
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ISender _sender;
        protected ISender sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var query = new GetProductsQuery();

            var products = await sender.Send(query);
            
            return products;
        }

        
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
        {
            await sender.Send(command, cancellationToken);

            return Ok();
        }
        
        [HttpPost("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
        {
            await sender.Send(command, cancellationToken);

            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductCommand command, CancellationToken cancellationToken)
        {
            await sender.Send(command, cancellationToken);

            return Ok();
        }
        
    }
}
