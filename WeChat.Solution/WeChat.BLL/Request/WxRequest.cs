using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Common;
using WeChat.Entity;

namespace WeChat.BLL
{
    public class WxRequest
    {
        #region 请求消息
        /// <summary>
        /// 请求消息
        /// </summary>
        /// <param name="param"></param>
        /// <param name="bug"></param>
        /// <returns></returns>
        public static BaseRequestMessage Request(EnterParamEntity param, bool bug = true)
        {
            using (StreamReader sr = new StreamReader(param.InputStream, Encoding.UTF8))
            {
                string data = string.Empty;
                try
                {
                    string requestXml = sr.ReadToEnd();
                    LogHelper.WriteLog("【微信POST】" + requestXml, LogMessageType.Info);
                    //安全模式下 
                    if (!string.IsNullOrWhiteSpace(param.encrypt_type) && param.encrypt_type.ToUpperInvariant() == "AES")
                    {
                        //加密模式
                        param.IsAes = true;
                        //解密消息
                        var result = new WXBizMsgCrypt(param.token, param.EncodingAESKey, param.appid).
                            DecryptMsg(param.msg_signature, param.timestamp, param.nonce, requestXml, ref data);
                        if (result != 0)
                        {
                            LogHelper.WriteLog("【微信POST】,消息解密失败！", LogMessageType.Error);
                        }
                    }
                    else
                    {
                        //明文模式
                        param.IsAes = false;
                        data = requestXml;
                    }
                    if (bug)
                    {
                        LogHelper.WriteLog("【微信POST】,消息体：" + data, LogMessageType.Info);
                    }
                    return MessageFactory.CreateMessage(data);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog("处理【微信消息】返回错误", ex);
                    return new BaseRequestMessage() { };
                }
            }
        }
        #endregion
    }
}
