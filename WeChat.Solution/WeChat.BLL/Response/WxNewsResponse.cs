﻿using System;
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
    ///【图文回复】 
    /// </summary>
    public class WxNewsResponse : WxBaseResponse
    {

        #region 微信响应方法
        /// <summary>
        /// 微信响应方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override string Response(BaseResponseMessage response, EnterParamEntity param)
        {
            var _Response = response as NewsResponseMessage;
            StringBuilder result = new StringBuilder();
            result.AppendFormat("<xml>");
            result.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", _Response.ToUserName);
            result.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", _Response.FromUserName);
            result.AppendFormat("<CreateTime>{0}></CreateTime>", _Response.CreateTime);
            result.AppendFormat("<MsgType><![CDATA[{0}]]></MsgType>", _Response.MsgType);
            result.AppendFormat("<ArticleCount>{0}</ArticleCount>", _Response.ArticleCount);
            result.AppendFormat("<Articles>");
            foreach (var item in _Response.Articles)
            {
                result.AppendFormat("<item>");
                result.AppendFormat("<Title><![CDATA[{0}]]></Title>", item.Title);
                result.AppendFormat("<Description><![CDATA[{0}]]></Description>", item.Description);
                result.AppendFormat("<PicUrl><![CDATA[{0}]]></PicUrl>", item.PicUrl);
                result.AppendFormat("<Url><![CDATA[{0}]]></Url>", item.Url);
                result.AppendFormat("</item>");
            }
            result.AppendFormat("</Articles>");
            result.AppendFormat("</xml>");
            LogHelper.WriteLog("【微信Response】响应消息明文：" + result.ToString(), LogMessageType.Info);
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
