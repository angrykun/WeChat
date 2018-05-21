﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Entity
{
    public class BaseMessage
    {
        /// <summary>
        /// 发送者标识
        /// </summary>
        public string FromUser { get; set; }
        /// <summary>
        /// 消息表示 普通消息时 为msgid，事件消息时为事件创建时间
        /// </summary>
        public string MsgFlag { get; set; }
        /// <summary>
        /// 添加到队列时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
