using EMS.EmailReader.Models;

namespace EMS.EmailReader.Services
{
    public interface IEmailCategorizer
    {
        Task DetermineEmailCategory();
    }
}
