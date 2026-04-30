
namespace EMS.Domain.Enums
{
    public enum EmailStatus
    {
        New,
        Read,    //read and store attachments
        Categorized,  // determine email category
        Assigned,       //assign to user
        Resolved       
    }
}
