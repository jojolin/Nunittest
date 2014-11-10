using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NUnitTest.Test
{
    public class StubService:IWebService
    {
        public Exception ToThrow;

        public void LogError(string message)
        {
            if (ToThrow != null)
            {
                throw ToThrow;
            }

            return;
        }
    }
}
