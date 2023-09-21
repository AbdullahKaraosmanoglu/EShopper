using EShopper.Layers;
using EShopper.Models;
using System.Web.Mvc;

namespace EShopper.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        public new ActionResult Profile(int userId)
        {
            TempData["name"] = "Abdullah";

            UserProcess userProcess = new UserProcess();
            var responseUserModel = userProcess.GetUserModelByUserId(userId);
            return View(responseUserModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileUpdate(UsersModel usersModel)
        {
            if (ModelState.IsValid)
            {
                var userProcess = new UserProcess();
                bool isUpdated = userProcess.UpdateUsers(usersModel);

                if (isUpdated)
                {
                    var updatedUser = userProcess.GetUserModelByUserId(usersModel.UserId);
                    TempData["AlertMessage"] = "Mükemmel!!";
                    return View("~/Views/Account/Profile.cshtml", updatedUser);
                }
            }

            // Eğer güncelleme başarısızsa veya model geçerli değilse aynı sayfayı tekrar göster
            return View("~/Views/Account/Profile.cshtml", usersModel);
        }

    }
}