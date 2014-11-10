using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using NUnit.Framework;

namespace NUnitTest
{
    /// <summary>
    /// NUnitTest.xaml 的交互逻辑
    /// </summary>
    public partial class AddTest : Window
    {
        public AddTest()
        {
            InitializeComponent();
        }
        public void func1()
        {
            AddClassTest t = new AddClassTest();
            t.Add();
            
            return;
        }
        
    }
    [TestFixture]
    public class TestTest
    {
        AddClassTest t = new AddClassTest();

        [Test]
        public void func1()
        {
            t.Add();
            return;
        }
    }


    [TestFixture]//注明这个类是单元测试类，这样NUnit测试工具可以找到这个类
    public class AddClassTest
    {
        AddClass ac = new AddClass();

        [SetUp]//注明这是测试初始化函数，每一个测试运行前都会运行这个函数
        public void Init()
        {
        }
        [Test]//注明这是一个测试，测试工具会自动运行这个函数进行测试
        public void Add()
        {
            Assert.AreEqual(2, ac.add(1, 1));//例子，验证1+1=2...
        }
        [Test]//另一个测试，和上面的Add无关
        public void Sub()
        {
            Assert.AreEqual(2, ac.sub(3, 2));//例子，验证1-1=2是否成立，显然这里会出现一个错误
        }

        [Test]
        public void NoEqual()
        {
            Assert.AreSame(ac, ac);
        }

        public class AddClass
        {
            public AddClass() { }

            public int add(int a, int b)
            {
                return a + b;
            }

            public int sub(int a, int b)
            {
                return a - b;
            }
        }
    }
}
