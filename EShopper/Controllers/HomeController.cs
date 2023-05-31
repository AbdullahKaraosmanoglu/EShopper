using EShopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShopper.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            UsersModel model = new UsersModel();
            return View(model);
        }
    }
}