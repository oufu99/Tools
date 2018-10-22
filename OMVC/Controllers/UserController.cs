using OMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            using (var db=new ModelDbContext())
            {
                User user = new User();
                user.Id = 1;
                user.Name = "Aaron";
                db.Users.Add(user);
                db.SaveChanges();
            }
           
            return Content("成功");
        }
    }
}