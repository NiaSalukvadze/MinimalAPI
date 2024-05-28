using MediatR;
using MinimalAPIwithCQRS_EF_Dapper.Models;

namespace MinimalAPIwithCQRS_EF_Dapper.CQRS.Commands.CreateProductCommand
{
    public record CreateProductCommand(Product Product) : IRequest;

}
