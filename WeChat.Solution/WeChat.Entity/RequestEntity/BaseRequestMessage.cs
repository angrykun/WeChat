using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeChat.Entity
{
    /// <summary>
    /// 请求消息基类
    /// </summary>
    [Serializable]
    [XmlRoot("xml")]
    public class BaseRequestMessage
    {
        /// <summary>
        ///  开发者微信号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        ///  发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        ///  消息创建时间 （整型）
        /// </summary>
        public long CreateTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }
        /// <summary>
        ///  消息id，64位整型
        /// </summary>
        public Int64 MsgId { get; set; }
    }
}
