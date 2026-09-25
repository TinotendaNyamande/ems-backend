using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategoryMatrix;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.CreateMatrix
{
    public record CreateMatrixCommand(string UserId,Guid EmailCategoryId):ICommand<GetMatrixDto>{}
}