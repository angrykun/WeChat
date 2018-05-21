using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeChat.Common;
using WeChat.Entity;
using WeChat.BLL;

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
            //WeChat.Common.LogHelper.WriteLog("UnitTest测试错误", Common.LogMessageType.Debug);
            //反序列化XML
            var xml = @"<xml><ToUserName><![CDATA[gh_94bf479fc602]]></ToUserName>
                        <FromUserName><![CDATA[owBQ61caU8ejg-PrgDa1YavOjdcY]]></FromUserName>
                        <CreateTime>1522131024</CreateTime>
                        <MsgType><![CDATA[text]]></MsgType>
                        <Content><![CDATA[gg]]></Content>
                        <MsgId>6537502968753845157</MsgId>
                        </xml>";
            var result = Utils.ConvertObj<TextRequestMessage>(xml);
        }


        [TestMethod]
        public void DeseriableToXML()
        {
            var model = new TextResponseMessage
            {
                Content = "6666",
                //CreateTime = 123423,
                //FromUserName = "dfd",
                MsgType = "fff",
                ToUserName = "cc"
            };

            //string result = new WxTextResponse().Response(model);

        }

        [TestMethod]
        public void MediaTest()
        {
            MediaApi api = new MediaApi();
            string result = api.GetBatchMaterial("image");
        }
    }
}
