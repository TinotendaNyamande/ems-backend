using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.CreateMatrix
{
    public record CreateMatrixCommand(string UserId,Guid EmailCategoryId):IRequest{}
}