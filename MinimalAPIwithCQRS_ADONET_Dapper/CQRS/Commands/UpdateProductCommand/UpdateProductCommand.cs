using MediatR;
using MinimalAPIwithCQRS_ADONET_Dapper.Models;

namespace MinimalAPIwithCQRS_ADONET_Dapper.CQRS.Commands.UpdateProductCommand
{
    public record UpdateProductCommand(Product Product) : IRequest<Unit>;
}
