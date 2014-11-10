using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NUnitTest.Test
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
}
