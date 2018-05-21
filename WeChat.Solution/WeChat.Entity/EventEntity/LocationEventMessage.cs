using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Entity
{
    public class LocationEventMessage : BaseRequestMessage
    {
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 地理位置精度
        /// </summary>
        public string Precision { get; set; }
    }
}
