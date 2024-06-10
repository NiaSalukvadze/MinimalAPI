using MediatR;
using MinimalAPIwithCQRS_Dapper_ADONET.Models;

namespace MinimalAPIwithCQRS_Dapper_ADONET.CQRS.Commands.CreateProductCommand
{
    public record CreateProductCommand(Product Product) : IRequest;

}
