using MediatR;
using MinimalAPIwithCQRS_Dapper_EF.Models;

namespace MinimalAPIwithCQRS_Dapper_EF.CQRS.Commands.UpdateProductCommand
{
    public record UpdateProductCommand(Product Product) : IRequest;
}
