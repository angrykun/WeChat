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
    /// 【音乐回复】
    /// </summary>
    public class WxMusicResponse : WxBaseResponse
    {
        #region 微信响应方法
        /// <summary>
        /// 微信响应方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override string Response(BaseResponseMessage response, EnterParamEntity param)
        {
            var _Response = response as MusicResponseMessage;
            StringBuilder result = new StringBuilder();
            result.AppendFormat("<xml>");
            result.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", _Response.ToUserName);
            result.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", _Response.FromUserName);
            result.AppendFormat("<CreateTime>{0}></CreateTime>", _Response.CreateTime);
            result.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", _Response.MsgType);
            result.AppendFormat("<Music>");
            if (!string.IsNullOrWhiteSpace(_Response.Title))
            {
                result.AppendFormat("<Title><![CDATA[{0}]]></Title>", _Response.Title);
            }
            if (!string.IsNullOrWhiteSpace(_Response.Description))
            {
                result.AppendFormat("<Description><![CDATA[{0}]]></Description>", _Response.Description);
            }
            if (!string.IsNullOrWhiteSpace(_Response.MusicURL))
            {
                result.AppendFormat("<MusicUrl><![CDATA[{0}]]></MusicUrl>", _Response.MusicURL);
            }
            if (!string.IsNullOrWhiteSpace(_Response.HQMusicUrl))
            {
                result.AppendFormat("<HQMusicUrl><![CDATA[{0}]]></HQMusicUrl>", _Response.HQMusicUrl);
            }
            if (!string.IsNullOrWhiteSpace(_Response.Description))
            {
                result.AppendFormat("<ThumbMediaId><![CDATA[{0}]]></ThumbMediaId>", _Response.ThumbMediaId);
            }
            result.AppendFormat("</Music>");
            result.AppendFormat("</xml>");
            if (param.IsAes)
            {
                string data = string.Empty;
                int encrypt = new WXBizMsgCrypt(param.token, param.EncodingAESKey, param.appid).
                    EncryptMsg(result.ToString(), _Response.CreateTime.ToString(), param.nonce, ref data);
                result = new StringBuilder(data);
                if (encrypt != 0)
                {
                    LogHelper.WriteLog("【微信响应文本消息】加密失败", LogMessageType.Error);
                }
            }
            return result.ToString();
        }
        #endregion
    }
}
