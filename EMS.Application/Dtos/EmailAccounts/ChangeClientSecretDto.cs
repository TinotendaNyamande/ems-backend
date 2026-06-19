namespace EMS.Application.Dtos.EmailAccounts
{
    public class ChangeClientSecretDto
    {
        public string OldSecret { get; set; }
        public string NewSecret { get; set; }
    }
}
