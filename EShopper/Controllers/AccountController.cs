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
            var responseUserModel = userProcess.GetUserModelByUserId(userId);
            return View(responseUserModel);
        }

        [HttpPost]
        public ActionResult ProfileUpdate(UsersModel usersModel)
        {
            UserProcess UserProcess = new UserProcess();
            var ResponseUserProcess = UserProcess.UpdateUsers(usersModel);
            var responseUserModel = UserProcess.GetUserModelByUserId(usersModel.UserId);
            return View("~/Views/Account/Profile.cshtml", responseUserModel);
        }
    }
}