using MediatR;
using MinimalAPIwithCQRS_ADONET_Dapper.Models;

namespace MinimalAPIwithCQRS_ADONET_Dapper.CQRS.Commands.CreateProductCommand
{
    public record CreateProductCommand(Product Product) : IRequest;

}
