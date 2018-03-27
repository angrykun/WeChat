using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Entity;
using WeChat.Common;
using WeChat.Enums;
using System.Xml.Linq;

namespace WeChat.BLL
{
    /// <summary>
    /// 响应消息
    /// </summary>
    public class WxTextResponse : IWxResponse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string Execute(BaseRequestMessage request, EnterParamEntity param)
        {
            //
            //业务逻辑
            //
            var response = new TextResponseMessage
            {
                ToUserName = request.FromUserName,
                FromUserName = request.ToUserName,
                MsgType = MsgTypeEnum.TEXT.ToString().ToLower(),
                Content = "你好！我是公众号开发者，欢迎您关注本公众号，如有问题，请联系：13888888888"
            };
            return Response(response, param);
        }

        /// <summary>
        /// 微信响应方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string Response(BaseResponseMessage response, EnterParamEntity param)
        {
            var _Response = response as TextResponseMessage;
            StringBuilder result = new StringBuilder();
            result.AppendFormat("<xml>");
            result.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", _Response.ToUserName);
            result.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", _Response.FromUserName);
            result.AppendFormat("<CreateTime>{0}></CreateTime>", _Response.CreateTime);
            result.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", _Response.MsgType);
            result.AppendFormat("<Content><![CDATA[{0}]]></Content>", _Response.Content);
            result.AppendFormat("</xml>");
            if (param.IsAes)
            {
                string data = string.Empty;
                int encrypt = new WXBizMsgCrypt(param.token, param.EncodingAESKey, param.appid).
                    EncryptMsg(result.ToString(), _Response.CreateTime.ToString(), param.nonce, ref data);
                result = new StringBuilder(data);
                if (encrypt != 0)
                {
                    LogHelper.WriteLog("【微信】响应消息加密失败", LogMessageType.Error);
                }
            }
            return result.ToString();
        }
    }
}
