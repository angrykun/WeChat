using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Entity;

namespace WeChat.BLL
{
    /// <summary>
    /// 响应微信消息接口
    /// </summary>
    interface IWxResponse
    {      
        /// <summary>
        /// 响应消息
        /// </summary>
        /// <param name="response">请求消息</param>
        /// <param name="param">请求参数</param>
        /// <returns></returns>
          string Execute(BaseRequestMessage request, EnterParamEntity param);
        
        /// <summary>
        /// 响应消息
        /// </summary>
        /// <param name="response">响应消息</param>
        /// <param name="param">请求参数</param>
        /// <returns></returns>
        string Response(BaseResponseMessage response, EnterParamEntity param);
    }
}
