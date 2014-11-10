using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
namespace NUnitTest.Test
{
    public class LogAnalyzerUsingFactoryMethod
    {

        public bool IsValidLogFileName(string fileName)
        {
            return GetManager().IsVaid(fileName);
        }

        // 虚方法
        protected virtual IExtensionManager GetManager()
        {
            return new FileExtensionManager();
        }
    }
    public class TestableLogAnalyzer : LogAnalyzerUsingFactoryMethod
    {
        public IExtensionManager Manager;

        protected override IExtensionManager GetManager()
        {
            return Manager;
        }
    }

    [TestFixture]
    public partial class LogAnalyzerTest
    {
        /// <summary>
        /// 使用伪造工厂方法
        /// </summary>
        [Test]
        public void overrideTest()
        {
            StupExtensionManager stub = new StupExtensionManager();
            TestableLogAnalyzer logan = new TestableLogAnalyzer();
            logan.Manager = stub;
            bool result = logan.IsValidLogFileName("fileName.txt");
            Assert.IsFalse(result, "file name should be too short to be considered valid");
        }
    }
}
