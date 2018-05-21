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
    public enum MsgTypeRequestEnum
    {
        #region 消息请求类型
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
        #endregion

        #region 事件类型
        /// <summary>
        /// 事件类型
        /// </summary>
        [Description("事件类型")]
        EVENT
        #endregion
    }
    #endregion

    #region 微信消息类型
    /// <summary>
    /// 微信消息类型
    /// </summary>
    public enum MsgTypeResponseEnum
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
        /// 音乐消息
        /// </summary>
        [Description("音乐消息")]
        MUSIC,
        /// <summary>
        /// 图文消息
        /// </summary>
        [Description("图文消息")]
        NEWS
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


    #region 事件类型
    /// <summary>
    /// 事件类型
    /// </summary>
    public enum EventEnum
    {
        /// <summary>
        /// 非事件类型
        /// </summary>
        [Description("非事件类型")]
        NOEVENT,
        /// <summary>
        /// 订阅
        /// </summary>
        [Description("订阅")]
        SUBSCRIBE,
        /// <summary>
        /// 取消订阅
        /// </summary>
        [Description("取消订阅")]
        UNSUBSCRIBE,
        /// <summary>
        /// 扫描带参数的二维码
        /// </summary>
        [Description("扫描带参数的二维码")]
        SCAN,
        /// <summary>
        /// 地理位置
        /// </summary>
        [Description("地理位置")]
        LOCATION,
        /// <summary>
        /// 单击按钮
        /// </summary>
        [Description("单击按钮")]
        CLICK,
        /// <summary>
        /// 链接按钮
        /// </summary>
        [Description("链接按钮")]
        VIEW,
        /// <summary>
        /// 扫码推事件
        /// </summary>
        [Description("扫码推事件")]
        SCANCODE_PUSH,
        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框
        /// </summary>
        [Description("扫码推事件且弹出")]
        SCANCODE_WAITMSG,
        /// <summary>
        /// 弹出系统拍照发图
        /// </summary>
        [Description("弹出系统拍照发图")]
        PIC_SYSPHOTO,
        /// <summary>
        /// 弹出拍照或者相册发图
        /// </summary>
        [Description("弹出拍照或者相册发图")]
        PIC_PHOTO_OR_ALBUM,
        /// <summary>
        /// 弹出微信相册发图器
        /// </summary>
        [Description("扫码推事件")]
        PIC_WEIXIN,
        /// <summary>
        /// 弹出地理位置选择器
        /// </summary>
        [Description("弹出地理位置选择器")]
        LOCATION_SELECT,
        /// <summary>
        /// 模板消息推送
        /// </summary>
        [Description("模板消息推送")]
        TEMPLATESENDJOBFINISH
    }
    #endregion



}
