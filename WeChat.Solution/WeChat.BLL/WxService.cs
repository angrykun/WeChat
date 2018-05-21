using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Common;
using WeChat.Enums;
using WeChat.Entity;

namespace WeChat.BLL
{
    /// <summary>
    /// 核心处理类
    /// </summary>
    public class WxService
    {
        public WxService()
        { }

        private IWxResponse BaseResponse;
      
        #region 处理请求
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string ProcessRequest(EnterParamEntity param)
        {
            //解密成xml格式
            var requestXML = WxRequest.Request(param);
            if (string.IsNullOrWhiteSpace(requestXML))
            {
                LogHelper.WriteLog("【微信POST】,消息体为空！", LogMessageType.Info);
                return string.Empty;
            }

            //将XML转化成对应的实体类
            var requestModel = MessageFactory.CreateRequestMessage(requestXML);
            if (requestModel == null)
            {
                LogHelper.WriteLog("【微信POST】,解析消息体为空！", LogMessageType.Info);
                return string.Empty;
            }

            //响应微信请求消息
            BaseResponse = new WxResponse();
            string result = BaseResponse.Execute(requestModel, param);
            return result;
        }
        #endregion
    }
}
