using EMS.Application.Dtos.EmailCategoryMatrix;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailCategoriesUserMatrixRepository
    {
        Task AddUser(EmailCategoriesUserMatrix userMatrix);
        Task DeleteMatrix(Guid id);
        Task<IEnumerable<EmailCategoriesUserMatrix>> GetAllAvailableForCategory(Guid? categoryId);
        Task UserAssignedTaskAction(Guid matrxiId);
        Task<IEnumerable<GetMatrixDto>> GetMatrixForOrganisation(Guid organisationId);
        Task<IEnumerable<GetMatrixDto>> GetMatrixForUser(string id);
        Task<GetMatrixDto> GetMatrixById (Guid id);

    }
}