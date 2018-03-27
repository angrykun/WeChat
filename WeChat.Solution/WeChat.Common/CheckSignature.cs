using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace WeChat.Common
{
    public class CheckSignature
    {
        #region 验证微信签名
        /// <summary>
        /// 验证微信签名
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// </summary>
        /// <param name="token"></param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public bool Check(string token, string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };
            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = Sha1(tmpStr);
            if (tmpStr.ToUpperInvariant() == signature.ToUpperInvariant())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region Sha1 算法
        /// <summary>
        /// Sha1 算法
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Sha1(string data)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));
            StringBuilder enText = new StringBuilder();

            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }
            return enText.ToString();
        }
        #endregion
    }
}
