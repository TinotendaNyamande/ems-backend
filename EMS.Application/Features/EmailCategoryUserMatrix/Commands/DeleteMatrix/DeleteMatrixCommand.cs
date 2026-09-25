using EMS.Application.Abstractions;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.DeleteMatrix
{
    public record DeleteMatrixCommand(Guid Id):ICommand{}
}