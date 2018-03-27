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

        public ActionResult Index()
        {
           
            WeChat.Common.LogHelper.WriteLog("测试错误", Common.LogMessageType.Debug);
            return View();
        }

    }
}
