﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NUnitTest.Test
{
    public class MockEmailService:IEmailService
    {
        public string To;
        public string Subject;
        public string Body;

        public void SendEmail(string to,string subject,string body)
        {
            To = to;
            Subject = subject;
            Body = body;
            return;
        }
    }
}
