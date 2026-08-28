using EMS.Application.Interfaces;
using Microsoft.AspNetCore.DataProtection;

namespace EMS.Infrastructure.Repository
{
    internal class EncryptionService(IDataProtectionProvider provider) : IEncryptionService
    {
        private readonly IDataProtector _protector = provider.CreateProtector("EMS.EncryptionService.Purpose");

        public string DecryptData(string value)
        {
            return _protector.Unprotect(value);
        }

        public string EncryptData(string value)
        {
            return _protector.Protect(value);
        }
    }
}