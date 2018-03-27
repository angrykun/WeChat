using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WeChat.BLL;
using WeChat.Common;
using WeChat.Entity;

namespace WeChat.Web.Controllers
{
    public class WeiXinController : Controller
    {
        private static readonly string token = ConfigHelper.GetConfigValue("token");
        private static readonly string appID = ConfigHelper.GetConfigValue("AppID");
        private static readonly string aesKey = ConfigHelper.GetConfigValue("AESKey");
        /// <summary>
        /// 微信公众号 服务器配置
        /// </summary>
        /// <returns></returns>
        public void Index()
        {
            var param = new EnterParamEntity
            {
                echoString = Request.QueryString["echoStr"],
                signature = Request.QueryString["signature"],
                timestamp = Request.QueryString["timestamp"],
                nonce = Request.QueryString["nonce"],
                encrypt_type = Request.QueryString["encrypt_type"],
                msg_signature = Request.QueryString["msg_signature"],
                InputStream = Request.InputStream
            };

            //请求方式
            string method = Request.HttpMethod;
            //验签
            if (new CheckSignature().Check(param.token, param.signature, param.timestamp, param.nonce))
            {
                if (method.ToUpperInvariant() != "POST")
                {
                    #region 成为开发者第一步
                    //token 与公众号后台token保持一致

                    if (string.IsNullOrEmpty(token))
                    {
                        Response.Write("Token 错误");
                    }

                    //回传 echoString
                    Response.Write(param.echoString);
                    #endregion
                }
                else
                {
                    //Response.Write("success");
                    var model = WxRequest.Request(param);
                    var responseResult = MessageFactory.CreateResponseMessage(model, param);
                    LogHelper.WriteLog("【微信】响应消息：" + responseResult, LogMessageType.Info);
                    Response.Write(responseResult);
                }
            }//验签结束


        }

    }
}
