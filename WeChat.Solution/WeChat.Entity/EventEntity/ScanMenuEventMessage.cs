using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Entity
{
    public class ScanMenuEventMessage : BaseRequestMessage
    {
        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 扫码类型。qrcode是二维码，其他的是条码
        /// </summary>
        public string ScanType { get; set; }
        /// <summary>
        /// 扫描结果
        /// </summary>
        public string ScanResult { get; set; }
    }
}
