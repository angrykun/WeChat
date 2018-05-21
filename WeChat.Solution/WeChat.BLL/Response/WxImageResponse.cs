using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Common;
using WeChat.Entity;
using WeChat.Enums;

namespace WeChat.BLL
{
    /// <summary>
    /// 【图片回复】
    /// </summary>
    public class WxImageResponse : WxBaseResponse
    {

        #region 微信响应方法
        /// <summary>
        /// 微信响应方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override string Response(BaseResponseMessage response, EnterParamEntity param)
        {
            var _Response = response as ImageResponseMessage;
            StringBuilder result = new StringBuilder();
            result.AppendFormat("<xml>");
            result.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", _Response.ToUserName);
            result.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", _Response.FromUserName);
            result.AppendFormat("<CreateTime>{0}></CreateTime>", _Response.CreateTime);
            result.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", _Response.MsgType);
            result.AppendFormat("<Image>");
            result.AppendFormat("<MediaId><![CDATA[{0}]]></MediaId>", _Response.MediaId);
            result.AppendFormat("</Image>");
            result.AppendFormat("</xml>");
            if (param.IsAes)
            {
                string data = string.Empty;
                int encrypt = new WXBizMsgCrypt(param.token, param.EncodingAESKey, param.appid).
                    EncryptMsg(result.ToString(), _Response.CreateTime.ToString(), param.nonce, ref data);
                result = new StringBuilder(data);
                if (encrypt != 0)
                {
                    LogHelper.WriteLog("【微信响应图片消息】加密失败", LogMessageType.Error);
                }
            }
            return result.ToString();
        }
        #endregion

    }
}
