using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Entity;

namespace WeChat.BLL
{
    /// <summary>
    /// 响应信息抽象基类
    /// </summary>
    public abstract class WxBaseResponse
    {

        /// <summary>
        /// 响应消息
        /// </summary>
        /// <param name="response">响应消息实体</param>
        /// <param name="param">微信请求参数</param>
        /// <returns>响应消息</returns>
        public abstract string Response(BaseResponseMessage response, EnterParamEntity param);
    }
}
