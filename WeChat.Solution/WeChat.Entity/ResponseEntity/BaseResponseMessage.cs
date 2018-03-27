using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Common;

namespace WeChat.Entity
{
    /// <summary>
    /// 响应消息基类
    /// </summary>
    public class BaseResponseMessage
    {
        /// <summary>
        ///  接收方帐号（收到的OpenID）
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        ///  开发者微信号
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        ///  消息创建时间 （整型）
        /// </summary>
        public long CreateTime
        {
            get
            {
                return Utils.DateTimeToInt;
            }
        }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }

    }
}
