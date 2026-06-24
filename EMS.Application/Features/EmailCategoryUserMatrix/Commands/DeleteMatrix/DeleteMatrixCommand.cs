using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.DeleteMatrix
{
    public record DeleteMatrixCommand(Guid Id):IRequest{}
}