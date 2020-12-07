using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WEBPO.Core.Interfaces
{
    public interface IEmailService
    {
        public Task<bool> SendForResetPassword(ICollection<string> receiver, string subject, string message);
    }
}
