using EShopper.Layers;
using EShopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShopper.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
  
        public new ActionResult Profile(int userId)
        {
            UserProcess userProcess = new UserProcess();
            UsersModel model = new UsersModel();
            var responseUserModel = userProcess.GetUserModelByUserId(userId);
            model = responseUserModel;

            return View(model);
        }
    }
}