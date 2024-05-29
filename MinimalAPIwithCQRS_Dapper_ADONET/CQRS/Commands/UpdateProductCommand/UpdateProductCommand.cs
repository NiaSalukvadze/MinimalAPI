using MediatR;
using MinimalAPIwithCQRS_Dapper_ADONET.Models;

namespace MinimalAPIwithCQRS_Dapper_ADONET.CQRS.Commands.UpdateProductCommand
{
    public record UpdateProductCommand(Product Product) : IRequest;
}
