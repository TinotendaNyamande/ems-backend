using EMS.Application.Abstractions;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.DeleteMatrix
{
    public record DeleteMatrixCommand(Guid Id):ICommand{}
}