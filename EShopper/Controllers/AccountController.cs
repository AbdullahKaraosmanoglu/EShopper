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
            TempData["name"] = "Bill";

            UserProcess userProcess = new UserProcess();
            var responseUserModel = userProcess.GetUserModelByUserId(userId);
            return View(responseUserModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileUpdate(UsersModel usersModel, [Bind(Include = "UserId,Name,Surname,Address,Email,Password,DateOfBirth,Gender ")] UsersModel users)
        {

            UserProcess UserProcess = new UserProcess();
            var ResponseUserProcess = UserProcess.UpdateUsers(usersModel);
            var responseUserModel = UserProcess.GetUserModelByUserId(usersModel.UserId);
            TempData["AlertMessage"] = "";
            //usersModel = (EShopper.Models.UsersModel)TempData["usersModel"];
            //TempData["usersModel"] = "Güncellendi";
            return View("~/Views/Account/Profile.cshtml", responseUserModel);
        }
    }
}