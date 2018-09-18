using OMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMVC.Controllers
{
    public class HomeController : Controller
    {
      
        
        public ActionResult Index()
        {

            string logMsg = string.Format("服务开始调度");
            LogHelper.LogInfo("ServiceLogger", logMsg);
            return Content(logMsg); 
        }

        
    }
}