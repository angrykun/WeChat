using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CM = System.Configuration.ConfigurationManager;

namespace WeChat.Common
{
    /// <summary>
    /// 配置文件Helper
    /// </summary>
    public class ConfigHelper
    {
        #region 获取webConfig中appSettings中配置项
        /// <summary>
        /// 获取webConfig中appSettings中配置项
        /// </summary>
        /// <param name="key">配置项的键</param>
        /// <returns>配置项的值</returns>
        public static string GetConfigValue(string key)
        {
            string result = string.Empty;
            try
            {
                result = CM.AppSettings[key];
            }
            catch { }
            return result;
        }
        #endregion

        #region 获取webconfig当中appsettings中的配置项
        /// <summary>
        ///  获取webconfig当中appsettings中的配置项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">配置项的键</param>
        /// <returns>配置项的值</returns>
        public static T GetConfigValue<T>(string key)
        {
            try
            {
                return (T)Convert.ChangeType(CM.AppSettings[key], typeof(T));
            }
            catch
            {
                return default(T);
            }
        }
        #endregion

    }
}
