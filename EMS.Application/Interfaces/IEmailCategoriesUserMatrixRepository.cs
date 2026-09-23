using EMS.Application.Dtos.EmailCategoryMatrix;
using EMS.Domain.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailCategoriesUserMatrixRepository
    {
        Task AddUserToMatrixAsync(EmailCategoriesUserMatrix userMatrix);
        Task DeleteMatrixAsync(Guid id);
        Task<IEnumerable<EmailCategoriesUserMatrix>> GetAllAvailableForCategoryAsync(Guid? categoryId);
        Task RecordUserAssignedTaskActionAsync(Guid matrxiId);
        Task<IEnumerable<GetMatrixDto>> GetMatrixForEmailAccountAsync(Guid emailAccountId);
        Task<IEnumerable<GetMatrixDto>> GetMatrixForUserAsync(string id);
        Task<GetMatrixDto> GetMatrixByIdAsync (Guid id);
        Task<bool> MatrixAlreadyExistsAsync(Guid categoryId, string userId);

    }
}