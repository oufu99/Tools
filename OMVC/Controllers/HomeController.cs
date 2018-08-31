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
      
        
        public ActionResult Index(RequestEnum renum)
        {
            return Content(renum.ToString()); 
        }

        
    }
}