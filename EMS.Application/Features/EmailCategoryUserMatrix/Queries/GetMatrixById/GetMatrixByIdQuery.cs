using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategoryMatrix;
using MediatR;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixById
{
    public record GetMatrixByIdQuery(Guid Id) : ICommand<GetMatrixDto>
    {
        
    }
}