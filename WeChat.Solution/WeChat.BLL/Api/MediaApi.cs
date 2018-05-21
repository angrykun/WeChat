using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat.Common;

namespace WeChat.BLL
{
    public class MediaApi : IWeiXin
    {
        public MediaApi(bool force = false)
            : base(force)
        {

        }
        public string GetBatchMaterial(string type)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}", AccessToken);

            var data = new
            {
                type = type,
                offset = 0,
                count = 20
            };
            //return new HttpMethod().HttpPost(url, Newtonsoft.Json.JsonConvert.SerializeObject(data), UTF8Encoding.UTF8, ref Cookie);
            string postData = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            return new HttpMethod().HttpPost(url, postData, UTF8Encoding.UTF8, ref Cookie);
        }
    }
}
