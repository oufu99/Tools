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
            TestMy.Name = "12木头人";

            return Content(TestMy.Name);
        }
        public ActionResult Index2()
        {
            return Content(TestMy.Name);
        }
        public ActionResult Index3()
        {
            TestMy.Name = "333";
            return Content(TestMy.Name);
        }

    }
    class TestMy
    {
        public static string Name { get; set; }

    }
}