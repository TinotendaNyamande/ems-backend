using EMS.Contracts.Events.Email;
using EMS.EmailCategorization.Worker.Models;

namespace EMS.EmailCategorization.Worker.Services
{
    public interface IEmailCategorizerService
    {
        Task<EmailCategoryResult> CategorizeAsync(string subject,string body,IEnumerable<string> categories,CancellationToken cancellationToken = default);
    }
}