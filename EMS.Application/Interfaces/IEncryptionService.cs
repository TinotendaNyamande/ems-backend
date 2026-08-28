namespace EMS.Application.Interfaces
{
    public interface IEncryptionService
    {
        public string EncryptData(string value);
        public string DecryptData(string value);
    }
}