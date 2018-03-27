using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Entity
{
    /// <summary>
    /// 图文消息
    /// </summary>
    public class NewsResponseMessage : BaseResponseMessage
    {
        /// <summary>
        /// 图文消息个数，限制为8条以内
        /// </summary>
        public string ArticleCount { get; set; }
        /// <summary>
        /// 多条图文消息信息，默认第一个item为大图,注意，如果图文数超过8，则将会无响应
        /// </summary>
        public List<Article> Articles { get; set; }


    }
}
