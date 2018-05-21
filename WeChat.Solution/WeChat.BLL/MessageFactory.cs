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
        private static List<BaseMessage> _Queue;

        #region 解析请求消息
        /// <summary>
        /// 解析请求消息
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static BaseRequestMessage CreateRequestMessage(string xml)
        {
            try
            {
                if (_Queue == null)
                {
                    _Queue = new List<BaseMessage>();
                }
                else if (_Queue != null && _Queue.Count > 50)
                {
                    //保留20秒内未响应信息
                    _Queue = _Queue.Where(q => { return q.CreateTime.AddSeconds(20) > DateTime.Now; }).ToList();
                }


                XElement xdoc = XElement.Parse(xml);
                var msgType = xdoc.Element("MsgType").Value.ToUpperInvariant();
                var FromUserName = xdoc.Element("FromUserName").Value;
                MsgTypeRequestEnum type = (MsgTypeRequestEnum)Enum.Parse(typeof(MsgTypeRequestEnum), msgType);
                if (type != MsgTypeRequestEnum.EVENT)
                {
                    var MsgId = xdoc.Element("MsgId").Value;
                    //消息类型
                    if (_Queue.FirstOrDefault(m => { return m.MsgFlag == MsgId; }) == null)
                    {
                        _Queue.Add(new BaseMessage
                        {
                            CreateTime = DateTime.Now,
                            FromUser = FromUserName,
                            MsgFlag = MsgId
                        });
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    var CreateTime = xdoc.Element("CreateTime").Value;
                    //事件类型
                    if (_Queue.FirstOrDefault(m => { return m.MsgFlag == CreateTime; }) == null)
                    {
                        _Queue.Add(new BaseMessage
                        {
                            CreateTime = DateTime.Now,
                            FromUser = FromUserName,
                            MsgFlag = CreateTime
                        });
                    }
                    else
                    {
                        return null;
                    }
                }
                switch (type)
                {
                    //文本消息
                    case MsgTypeRequestEnum.TEXT:
                        return Utils.ConvertObj<TextRequestMessage>(xml);
                    //图片消息
                    case MsgTypeRequestEnum.IMAGE:
                        return Utils.ConvertObj<ImageRequestMessage>(xml);
                    //视频消息
                    case MsgTypeRequestEnum.VIDEO:
                        return Utils.ConvertObj<VideoRequestMessage>(xml);
                    //链接消息
                    case MsgTypeRequestEnum.LINK:
                        return Utils.ConvertObj<LinkRequestMessage>(xml);
                    //地理消息
                    case MsgTypeRequestEnum.LOCATION:
                        return Utils.ConvertObj<LocationRequestMessage>(xml);
                    //短视频消息
                    case MsgTypeRequestEnum.SHORTVIDEO:
                        return Utils.ConvertObj<ShortVideoRequestMessage>(xml);
                    //音频消息
                    case MsgTypeRequestEnum.VOICE:
                        return Utils.ConvertObj<VoiceRequestMessage>(xml);
                    //事件类型
                    case MsgTypeRequestEnum.EVENT:

                        #region 事件类型
                        var eventType = (EventEnum)Enum.Parse(typeof(EventEnum), xdoc.Element("Event").Value.ToUpperInvariant());
                        switch (eventType)
                        {

                            case EventEnum.CLICK:
                            case EventEnum.VIEW:
                                return Utils.ConvertObj<NormalMenuEventMessage>(xml);

                            case EventEnum.LOCATION:
                            case EventEnum.LOCATION_SELECT:
                                return Utils.ConvertObj<LocationEventMessage>(xml);

                            case EventEnum.SCAN:
                                return Utils.ConvertObj<ScanEventMessage>(xml);

                            case EventEnum.SUBSCRIBE:
                            case EventEnum.UNSUBSCRIBE:
                                return Utils.ConvertObj<SubEventMessage>(xml);

                            case EventEnum.SCANCODE_WAITMSG:
                                return Utils.ConvertObj<ScanMenuEventMessage>(xml);

                            default: return Utils.ConvertObj<BaseRequestMessage>(xml); ;
                        }
                        #endregion

                    default: return Utils.ConvertObj<BaseRequestMessage>(xml);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("【微信】解析请求消息出错", ex);
                return new BaseRequestMessage { };
            }

        }
        #endregion

        #region 创建响应类型消息
        /// <summary>
        /// 创建响应类型消息
        /// </summary>
        /// <param name="requestMessage">请求消息</param>
        /// <param name="param">请求参数</param>
        /// <param name="msgType">响应消息类型</param>
        /// <returns></returns>
        //public static string CreateResponseModel(BaseResponseMessage responseMessage, MsgTypeResponseEnum msgType)
        //{
        //    try
        //    {
        //        WxBaseResponse response = null;
        //        #region 响应类型
        //        switch (msgType)
        //        {
        //            //文本消息
        //            case MsgTypeResponseEnum.TEXT:
        //                response = new WxTextResponse();
        //                break;
        //            //图片消息
        //            case MsgTypeResponseEnum.IMAGE:
        //                response = new WxImageResponse();
        //                break;
        //            //视频消息
        //            case MsgTypeResponseEnum.VIDEO:
        //                response = new WxVideoResponse();
        //                break;
        //            //音乐消息
        //            case MsgTypeResponseEnum.MUSIC:
        //                response = new WxMusicResponse();
        //                break;
        //            //图文消息
        //            case MsgTypeResponseEnum.NEWS:
        //                response = new WxNewsResponse();
        //                break;
        //            //语音消息
        //            case MsgTypeResponseEnum.VOICE:
        //                response = new WxVoiceResponse();
        //                break;
        //            //默认回复文本消息
        //            default:
        //                response = new WxTextResponse();
        //                break;
        //        }

        //        #endregion
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteLog("【微信】响应消息出错", ex);
        //        return string.Empty;
        //    }
        //}
        #endregion

        #region 创建响应类型消息
        /// <summary>
        /// 创建响应类型消息
        /// </summary>
        /// <param name="requestMessage">请求消息</param>
        /// <param name="param">请求参数</param>
        /// <param name="msgType">响应消息类型</param>
        /// <returns></returns>
        public static string CreateResponseType(BaseResponseMessage responseMessage, EnterParamEntity param, MsgTypeResponseEnum msgType)
        {
            try
            {
                WxBaseResponse response = null;
                #region 响应类型
                switch (msgType)
                {
                    //文本消息
                    case MsgTypeResponseEnum.TEXT:
                        response = new WxTextResponse();
                        break;
                    //图片消息
                    case MsgTypeResponseEnum.IMAGE:
                        response = new WxImageResponse();
                        break;
                    //视频消息
                    case MsgTypeResponseEnum.VIDEO:
                        response = new WxVideoResponse();
                        break;
                    //音乐消息
                    case MsgTypeResponseEnum.MUSIC:
                        response = new WxMusicResponse();
                        break;
                    //图文消息
                    case MsgTypeResponseEnum.NEWS:
                        response = new WxNewsResponse();
                        break;
                    //语音消息
                    case MsgTypeResponseEnum.VOICE:
                        response = new WxVoiceResponse();
                        break;
                    //默认回复文本消息
                    default:
                        response = new WxTextResponse();
                        break;
                }

                #endregion
                return response.Response(responseMessage, param);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("【微信】响应消息出错", ex);
                return string.Empty;
            }
        }
        #endregion

    }
}
