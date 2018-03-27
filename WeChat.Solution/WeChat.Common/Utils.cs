using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using WeChat.Enums;

namespace WeChat.Common
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Utils
    {

        /// <summary>
        /// 获取时间 
        /// </summary>
        public static long DateTimeToInt
        {
            get
            {
                //设置初始时间  
                DateTime start = new DateTime(1970, 1, 1);
                long cur = (long)(DateTime.Now - start).TotalSeconds;
                return cur;
            }

        }

        #region MyRegion
        /// <summary>
        /// xml 转成T类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T ConvertObj<T>(string xml)
        {
            XElement xdoc = XElement.Parse(xml);
            var type = typeof(T);
            var t = Activator.CreateInstance<T>();
            foreach (XElement element in xdoc.Elements())
            {
                var pr = type.GetProperty(element.Name.ToString());
                if (element.HasElements)
                {
                    //这里主要是兼容微信新添加的菜单类型。nnd，竟然有子属性，所以这里就做了个子属性的处理
                    foreach (var ele in element.Elements())
                    {
                        pr = type.GetProperty(ele.Name.ToString());
                        pr.SetValue(t, Convert.ChangeType(ele.Value, pr.PropertyType), null);
                    }
                }
                if (pr.PropertyType.Name == "MsgType")//获取消息模型
                {
                    pr.SetValue(t, (MsgTypeEnum)Enum.Parse(typeof(MsgTypeEnum), element.Value.ToUpper()), null);
                    continue;
                }
                if (pr.PropertyType.Name == "Event")//获取事件类型。
                {
                    pr.SetValue(t, (EventEnum)Enum.Parse(typeof(EventEnum), element.Value.ToUpper()), null);
                    continue;
                }
                pr.SetValue(t, Convert.ChangeType(element.Value, pr.PropertyType), null);
            }
            return t;
        }
        #endregion
    }
}
