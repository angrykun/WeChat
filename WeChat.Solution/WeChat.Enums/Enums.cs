using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Enums
{
    #region 微信消息类型
    /// <summary>
    /// 微信消息类型
    /// </summary>
    public enum MsgTypeEnum
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        [Description("文本消息")]
        TEXT,
        /// <summary>
        /// 图片消息
        /// </summary>
        [Description("图片消息")]
        IMAGE,
        /// <summary>
        /// 语音消息
        /// </summary>
        [Description("语音消息")]
        VOICE,
        /// <summary>
        /// 视频消息
        /// </summary>
        [Description("视频消息")]
        VIDEO,
        /// <summary>
        /// 小视频消息
        /// </summary>
        [Description("小视频消息")]
        SHORTVIDEO,
        /// <summary>
        /// 地理位置消息
        /// </summary>
        [Description("地理位置消息")]
        LOCATION,
        /// <summary>
        /// 链接消息
        /// </summary>
        [Description("链接消息")]
        LINK,
        /// <summary>
        /// 事件类型
        /// </summary>
        [Description("事件类型")]
        EVENT
    }
    #endregion


    #region 微信语音消息格式
    public enum VoiceFormatEnum
    {
        /// <summary>
        /// speex
        /// </summary>
        [Description("amr")]
        amr,   /// <summary>
        /// speex
        /// </summary>
        [Description("speex")]
        speex
    }
    #endregion


    #region MyRegion
    public enum EventEnum
    { }
    #endregion



}
