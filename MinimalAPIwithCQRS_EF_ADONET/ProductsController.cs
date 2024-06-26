﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIwithCQRS_EF_ADONET.CQRS.Commands.CreateProductCommand;
using MinimalAPIwithCQRS_EF_ADONET.CQRS.Commands.DeleteProductCommand;
using MinimalAPIwithCQRS_EF_ADONET.CQRS.Commands.UpdateProductCommand;
using MinimalAPIwithCQRS_EF_ADONET.CQRS.Queries.GetProducts;

namespace MinimalAPIwithCQRS_EF_ADONET
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
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
        public async Task<IActionResult> DeleteProduct(CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand();

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
