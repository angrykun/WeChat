using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChat.Common;
using WeChat.Entity;

namespace WeChat.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public enum Enums
        {
            TEXT,
            EVENT
        }
        public ActionResult Index()
        {
            Enums _msgType= Enums.EVENT;
            Enum.TryParse("textt", true, out _msgType);
            WeChat.Common.LogHelper.WriteLog("测试错误", Common.LogMessageType.Debug);
            return View();
        }

    }
}
