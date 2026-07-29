namespace EMS.EmailCategorization.Worker.Models
{
    public sealed class EmailCategoryResult
    {
        public string Category { get; set; } = "";
        public string Reason { get; set; } = "";
    }
}