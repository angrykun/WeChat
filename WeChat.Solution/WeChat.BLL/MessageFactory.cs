using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WeChat.Common;
using WeChat.Enums;
using WeChat.Entity;

namespace WeChat.BLL
{
    public class MessageFactory
    {

        #region 解析请求消息
        public static BaseRequestMessage CreateMessage(string xml)
        {
            XElement xdoc = XElement.Parse(xml);
            var msgType = xdoc.Element("MsgType").Value.ToUpperInvariant();
            MsgTypeEnum type = (MsgTypeEnum)Enum.Parse(typeof(MsgTypeEnum), msgType);
            switch (type)
            {
                //文本消息
                case MsgTypeEnum.TEXT:
                    return Utils.ConvertObj<TextRequestMessage>(xml);
                //图片消息
                case MsgTypeEnum.IMAGE:
                    return Utils.ConvertObj<ImageRequestMessage>(xml);
                //视频消息
                case MsgTypeEnum.VIDEO:
                    return Utils.ConvertObj<VideoRequestMessage>(xml);
                //链接消息
                case MsgTypeEnum.LINK:
                    return Utils.ConvertObj<LinkRequestMessage>(xml);
                //地理消息
                case MsgTypeEnum.LOCATION:
                    return Utils.ConvertObj<LocationRequestMessage>(xml);
                //短视频消息
                case MsgTypeEnum.SHORTVIDEO:
                    return Utils.ConvertObj<ShortVideoRequestMessage>(xml);
                //音频消息
                case MsgTypeEnum.VOICE:
                    return Utils.ConvertObj<VoiceRequestMessage>(xml);
                default: return Utils.ConvertObj<BaseRequestMessage>(xml);
            }
        }
        #endregion

        public static string CreateResponseMessage(BaseRequestMessage requestMessage, EnterParamEntity param)
        {
            var msgType = requestMessage.MsgType.ToUpperInvariant();
            MsgTypeEnum type = (MsgTypeEnum)Enum.Parse(typeof(MsgTypeEnum), msgType);
            IWxResponse response;
            switch (type)
            {
                //文本消息
                case MsgTypeEnum.TEXT:
                    response = new WxTextResponse();
                    return response.Execute(requestMessage, param);
                //图片消息
                case MsgTypeEnum.IMAGE:
                    return "";
                //视频消息
                case MsgTypeEnum.VIDEO:
                    return "";
                //链接消息
                case MsgTypeEnum.LINK:
                    return "";
                //地理消息
                case MsgTypeEnum.LOCATION:
                    return "";
                //短视频消息
                case MsgTypeEnum.SHORTVIDEO:
                    return "";
                //音频消息
                case MsgTypeEnum.VOICE:
                    return "";
                default: return "";
            }
        }
    }
}
