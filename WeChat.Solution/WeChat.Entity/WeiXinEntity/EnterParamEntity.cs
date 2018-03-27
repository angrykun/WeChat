using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using WeChat.Common;
using System.IO;
namespace WeChat.Entity
{
    /// <summary>
    /// 微信传入参数
    /// </summary>
    public class EnterParamEntity
    {
        public Stream InputStream { get; set; }
        /// <summary>
        /// 是否加密
        /// </summary>
        public bool IsAes { get; set; }
        private string _token;
        /// <summary>
        /// 接入token
        /// </summary>
        public string token
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_token))
                {
                    _token = ConfigHelper.GetConfigValue("token");
                }
                return _token;
            }
        }
        private string _appid;
        /// <summary>
        ///微信appid
        /// </summary>
        public string appid
        {
            get
            {

                if (string.IsNullOrWhiteSpace(_appid))
                {
                    _appid = ConfigHelper.GetConfigValue("AppID");
                }
                return _appid;
            }
        }
        private string _aESKey;
        /// <summary>
        /// 加密密钥
        /// </summary>
        public string EncodingAESKey
        {
            get
            {

                if (string.IsNullOrWhiteSpace(_aESKey))
                {
                    _aESKey = ConfigHelper.GetConfigValue("AESKey");
                }
                return _aESKey;
            }
        }
        public string encrypt_type { get; set; }
        public string msg_signature { get; set; }

        public string echoString { get; set; }
        public string signature { get; set; }
        public string timestamp { get; set; }
        public string nonce { get; set; }
    }
}
