using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeChat.Entity
{
    /// <summary>
    /// 文本消息
    /// </summary>
    [Serializable]

    public class TextRequestMessage : BaseRequestMessage
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }
    }
}
