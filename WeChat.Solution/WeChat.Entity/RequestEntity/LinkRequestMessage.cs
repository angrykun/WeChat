using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Entity
{
    /// <summary>
    /// 连接消息
    /// </summary>
    public class LinkRequestMessage : BaseRequestMessage
    {
        /// <summary>
        ///  消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///  消息描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///  消息链接
        /// </summary>
        public string Url { get; set; }
    }
}
