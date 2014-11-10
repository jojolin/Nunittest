using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using Rhino.Mocks;
namespace NUnitTest.Test
{
    public class LogAnalyzer
    {
        private IExtensionManager _manager;
        ExtensinManagerFactory mgrFactory = new ExtensinManagerFactory();
        public LogAnalyzer()
        {
            _manager = mgrFactory.Create();    // 生产代码中使用
        }

        /// <summary>
        /// 利用工厂方法调用
        /// </summary>
        public bool IsValidLogFileNameLength(string fileName)
        {
            return _manager.IsVaid(fileName) &&
                Path.GetFileNameWithoutExtension(fileName).Length > 5;
        }
        /// <summary>
        /// 利用工厂类注入桩对象
        /// </summary>
        [Test]
        public void IsValidLogFileNameLength_ShorterThan6_ReturnsFalse()
        {
            StupExtensionManager stubMgr = new StupExtensionManager();
            mgrFactory.SetManager(stubMgr);    // 将桩对象赋给工厂类

            LogAnalyzer log = new LogAnalyzer();

            bool res = log.IsValidLogFileNameLength("whatever.slf");

            Assert.IsTrue(res, "should more than 6 char");
            return;
        }

        /// <summary>
        /// 简单方法，不依赖文件系统
        /// </summary>
        public bool IsValidLogFileName(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("No fileName provided!");
            }
            if (!fileName.EndsWith(".SLF"))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 简单方法，不依赖文件系统
        /// </summary>
        public bool IsValidLogFileName2(string fileName)
        {
            if (!fileName.ToLower().EndsWith(".slf"))
            {
                WasLastFileNameValid = false;
                return false;
            }
            WasLastFileNameValid = true;
            return true;
        }
        public bool WasLastFileNameValid { get; set; }

        /// 交互测试 ： 模拟对象
        private IWebService _service;
        public LogAnalyzer(IWebService service)
        {
            _service = service;
        }

        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
            {
                _service.LogError("Filename too short:" + fileName);
            }
        }
    }



    /// <summary>
    /// 测试类
    /// </summary>
    [TestFixture]
    public partial class LogAnalyzerTest
    {
        private LogAnalyzer _analyzer = null;

        [SetUp]
        public void Setup()
        {
            _analyzer = new LogAnalyzer();
        }
        [TearDown]
        public void TearDown()
        {
            _analyzer = null;
        }
        [Test]
        [Category("运行快")]
        public void IsValidFileName_validFileLowerCased_ReturnsTrue()
        {
            bool result = _analyzer.IsValidLogFileName("whatever.slf");
            Assert.IsTrue(result, "filename should be valid!");
        }

        [Test]
        [Category("运行快")]
        public void IsValidFileName_validFileUpperCased_ReturnsTrue()
        {
            bool result = _analyzer.IsValidLogFileName("whatever.SLF");
            Assert.IsTrue(result, "filename should be valid!");
        }

        [Test]
        [Ignore("测试有问题")]
        public void IsValidFileName_validFileUpperCased_ReturnsTrue2()
        {
            bool result = _analyzer.IsValidLogFileName("whatever.SLF");
            Assert.IsTrue(result, "filename be valid!");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "No fileName provided!")]
        [Category("运行慢")]
        public void IsValidFileName_EmptyFileName_ThrowsException()
        {
            _analyzer.IsValidLogFileName(string.Empty);
            return;
        }

        [Test]
        public void IsValidLogFileName_ValidName_RemembersTrue()
        {
            LogAnalyzer log = new LogAnalyzer();
            log.IsValidLogFileName2("somefile.slf");
            Assert.IsTrue(log.WasLastFileNameValid);    // 验证属性值而非返回值
            return;
        }

        /// 交互测试
        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            MockService mockService = new MockService();
            LogAnalyzer log = new LogAnalyzer(mockService);
            string tooShortFileName = "abc.txt";
            log.Analyze(tooShortFileName);
            Assert.AreEqual("Filename too short:abc.txt", mockService.LastError);

            return;
        }

        /// rhino mocks 框架
        /// record-and-replay
        [Test]
        public void Analyze_TooShortFileName_ErrorLoggedToService() {
            MockRepository mocks = new MockRepository();
            IWebService simulatedService = mocks.StrictMock<IWebService>();
            using (mocks.Record())
            {
                simulatedService.LogError("Filename too short:abc.txt");    // 设置预期
            }

            LogAnalyzer log = new LogAnalyzer(simulatedService);
            string tooShortFileName = "abc.txt";
            log.Analyze(tooShortFileName);

            mocks.Verify(simulatedService);    // 断言已符合预期
        }

    }

}
