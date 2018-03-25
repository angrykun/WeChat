using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeChat.Web.Tests
{
    /// <summary>
    /// UnitTest1 的摘要说明
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        [TestMethod]
        public void TestMethod1()
        {
            WeChat.Common.LogHelper.WriteLog("UnitTest测试错误", Common.LogMessageType.Debug);
            //
            // TODO:  在此处添加测试逻辑
            //
        }
    }
}
