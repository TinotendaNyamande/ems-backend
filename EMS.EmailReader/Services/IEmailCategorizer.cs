using EMS.EmailReader.Models;

namespace EMS.EmailReader.Services
{
    public interface IEmailCategorizer
    {
        Task<EmailCategoryResult> CategorizeAsync(
            string subject,
            string body,
            IEnumerable<string> categories,
            CancellationToken cancellationToken = default);
    }
}
