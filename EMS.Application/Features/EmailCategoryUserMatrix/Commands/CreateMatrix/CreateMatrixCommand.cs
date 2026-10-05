using EMS.Application.Abstractions;
using EMS.Domain.Models;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Commands.CreateMatrix
{
    public record CreateMatrixCommand(string UserId,Guid EmailCategoryId):ICommand<EmailCategoriesUserMatrix>{}
}