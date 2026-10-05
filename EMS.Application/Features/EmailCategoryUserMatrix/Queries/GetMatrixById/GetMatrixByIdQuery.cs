using EMS.Application.Abstractions;
using EMS.Application.Dtos.EmailCategoryMatrix;

namespace EMS.Application.Features.EmailCategoryUserMatrix.Queries.GetMatrixById
{
    public record GetMatrixByIdQuery(Guid Id) : IQuery<GetMatrixDto>
    {
        
    }
}