using EMS.Application.Interfaces;
using Microsoft.AspNetCore.DataProtection;

namespace EMS.Infrastructure.Repository
{
    internal class EncryptionService(IDataProtector protector) : IEncryptionService
    {
        public string DescryptData(string value)
        {
            return protector.Unprotect(value);
        }

        public string EncryptData(string value)
        {
            return protector.Protect(value);
        }
    }
}