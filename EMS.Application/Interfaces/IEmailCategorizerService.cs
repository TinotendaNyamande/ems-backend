using EMS.Application.Models;

namespace EMS.Application.Interfaces
{
    public interface IEmailCategorizerService
    {
        Task<EmailCategoryResult> CategorizeAsync(string subject, string body, IEnumerable<string> categories);
    }
}