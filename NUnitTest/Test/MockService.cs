using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NUnitTest.Test
{
    public class MockService : IWebService
    {
        public string LastError;

        public void LogError(string message)
        {
            LastError = message;
            return;
        }


    }
}
