using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NUnitTest.Test
{
    /// <summary>
    /// 接口方法
    /// </summary>
    public interface IExtensionManager
    {
        bool IsVaid(string fileName);
    }

    public class FileExtensionManager : IExtensionManager
    {
        public bool IsVaid(string fileName)
        {
            // ...
            return true;
        }


        public bool IsValidLogFileName(string fileName)
        {
            IExtensionManager mgr = new FileExtensionManager();

            return mgr.IsVaid(fileName);
        }
    }

    public class StupExtensionManager : IExtensionManager
    {
        public bool IsVaid(string fileName)
        {
            return true;
        }
    }

    /// <summary>
    /// 工厂类： 注入桩对象并返回桩实例
    /// </summary>
    public class ExtensinManagerFactory
    {
        private IExtensionManager _ctmManager = null;

        public IExtensionManager Create()
        {
            if (_ctmManager == null)
            {
                return _ctmManager;
            }
            return new FileExtensionManager();
        }

        public void SetManager(IExtensionManager mgr)
        {
            _ctmManager = mgr;
        }
    }


}
