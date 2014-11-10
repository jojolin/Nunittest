using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
namespace NUnitTest.Test
{
    public class LogAnalyzer2
    {
        private IWebService _service;
        private IEmailService _email;

        public IWebService Service
        {
            get { return _service; }
            set { _service = value; }
        }
        public IEmailService Email
        {
            get { return _email; }
            set { _email = value; }
        }


        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
            {
                try
                {
                    _service.LogError("Filename too short:" + fileName);
                }
                catch (Exception e)
                {
                    _email.SendEmail("a", "subject", e.Message);
                }
            }
            return;
        }
    }

    [TestFixture]
    public class LogAnalyzer2Tests
    {   
        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            StubService stubService = new StubService();
            stubService.ToThrow = new Exception("fake exception");
            MockEmailService mockEmail = new MockEmailService();

            LogAnalyzer2 log = new LogAnalyzer2();
            log.Service = stubService;
            log.Email = mockEmail;
            string tooShortFileName = "abc.txt";
            log.Analyze(tooShortFileName);

            Assert.AreEqual("a", mockEmail.To);
            Assert.AreEqual("fake exception", mockEmail.Body);
            Assert.AreEqual("subject", mockEmail.Subject);

            return;
        }

    }

}
