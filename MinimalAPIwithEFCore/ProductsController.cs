using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPIwithEFCore.Models;
using System;

namespace MinimalAPIwithEFCore
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SalesContext _context;

        public ProductsController(SalesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        [HttpPost("update")]
        public async Task<ActionResult<Product>> UpdateProduct(Product prod)
        {
            var productId = prod.ProductId;

            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);

            product.ProductName = prod.ProductName;
            product.SupplierId = prod.SupplierId;
            product.CategoryId = prod.CategoryId;

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct()
        {
            var product = await _context.Products.OrderByDescending(x => x.ProductId).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
