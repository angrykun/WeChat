using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Common;

namespace WeChat.BLL
{
    public abstract class IWeiXin
    {
        protected static readonly string appID = ConfigHelper.GetConfigValue("AppID");
        protected static readonly string appSecret = ConfigHelper.GetConfigValue("AppSecret");
        /// <summary>
        /// Cookie
        /// </summary>
        public static string Cookie = string.Empty;
        /// <summary>
        /// AccessToken
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 对应微信后台开发者凭证的AppId
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// 对应微信后台开发者凭证的AppSecret
        /// </summary>
        public string AppSecret { get; set; }
        public IWeiXin(bool force)
        {
            AppID = appID;
            AppSecret = appSecret;

            AccessToken = GeneralAccessToken(force);
        }
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        public string GeneralAccessToken(bool force = false)
        {
            string tokenUrl = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appID, appSecret);
            string key = string.Format("WeChatProject_AccessToken");
            CacheAccessTokenInfo accessToken = null;
            string accessTokenString = CacheHelper.Instance.Get(key) as string;
            if (!string.IsNullOrWhiteSpace(accessTokenString) && !force)
            {
                try
                {
                    accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<CacheAccessTokenInfo>(accessTokenString);
                }
                catch { }
            }
            if (accessToken == null || string.IsNullOrWhiteSpace(accessToken.AccessToken) || DateTime.Now.Subtract(accessToken.CreateTime).TotalMinutes >= 100)
            {
                accessToken = new CacheAccessTokenInfo();
                accessTokenString = HttpClientHelperNew.Get(tokenUrl);
                //HttpClientHelper.Get(tokenUrl);
                accessToken.CreateTime = DateTime.Now;

                try
                {
                    var jObject = Newtonsoft.Json.JsonConvert.DeserializeObject(accessTokenString) as Newtonsoft.Json.Linq.JObject;
                    if (jObject != null) accessToken.AccessToken = jObject.Value<string>("access_token");
                }
                catch { }
                if (!string.IsNullOrWhiteSpace(accessToken.AccessToken))
                {
                    string cacheString = Newtonsoft.Json.JsonConvert.SerializeObject(accessToken);
                    CacheHelper.Instance.Update(key, cacheString, 115);
                }
            }
            return accessToken.AccessToken;
        }
    }
    public class CacheAccessTokenInfo
    {
        /// <summary>
        /// AccessToken
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>

        public DateTime CreateTime { get; set; }
    }

}
