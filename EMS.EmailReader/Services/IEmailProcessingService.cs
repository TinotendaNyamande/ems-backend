namespace EMS.EmailReader.Services
{
    public interface IEmailProcessingService
    {
        Task EmailReaderService();
        Task EmailCategoryService();
        Task TaskAssignmentService();
    }
}