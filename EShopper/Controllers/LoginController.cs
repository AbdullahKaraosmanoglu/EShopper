using EShopper.Layers;
using EShopper.Models;
using System;
using System.Web.Mvc;

namespace EShopper.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginProcess()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(UsersModel user)
        {
            try
            {
                if (!FormValidation())
                {
                    @ViewBag.ErrorMessage = "Tüm alanları doldurunuz.";

                    return View("~/Views/Login/LoginProcess.cshtml");
                }
                if (!SignUpControl())
                {
                    @ViewBag.SErrorMessage = "Aynı Email İle Kayıtlı Üye Mevcut Başka Bir Email İle Deneyiniz";
                    return View("~/Views/Login/LoginProcess.cshtml");
                }
                string TxtName = Request.Form["TxtName"].ToString();
                string TxtSurname = Request.Form["TxtSurname"].ToString();
                string TxtEmail = Request.Form["TxtEmail"].ToString();
                string TxtPassword = Request.Form["TxtPassword"].ToString();
                string DtDateOfBirth = Request.Form["DtDateOfBirth"].ToString();
                string TxtAddress = Request.Form["TxtAddress"].ToString();
                string slGender = Request.Form["slGender"].ToString();

                int gender = 0;

                switch (slGender)
                {
                    case "Man":
                        gender = 0;
                        break;
                    case "Woman":
                        gender = 1;
                        break;
                }


                UsersModel usersModel = new UsersModel
                {
                    Name = TxtName,
                    Surname = TxtSurname,
                    Email = TxtEmail,
                    Password = TxtPassword,
                    Address = TxtAddress,
                    DateOfBirth = Convert.ToDateTime(DtDateOfBirth),
                    Gender = gender
                };

                UserProcess userProcess = new UserProcess();
                userProcess.AddUser(usersModel);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            @ViewBag.SuccessMessage = "Kayıt İşlemi Başarıyla Tamamlandı Lütfen Giriş Yaparak Devam Ediniz";
            return View("~/Views/Login/LoginProcess.cshtml");
        }

        [HttpPost]
        public ActionResult LoginControl(UsersModel usersModel)
        {
            UserProcess userProcess = new UserProcess();
            string TxtEmail = Request.Form["TxtEmail"].ToString();
            string TxtPassword = Request.Form["TxtPassword"].ToString();
            try
            {

                if (!String.IsNullOrEmpty(TxtEmail) || !String.IsNullOrEmpty(TxtPassword))
                {
                    UsersModel usersModelGet = new UsersModel
                    {
                        Email = TxtEmail,
                        Password = TxtPassword
                    };

                    var response = userProcess.LoginControl(usersModelGet);

                    if (response == null || response.Count <= 0)
                    {
                        @ViewBag.LErrorMessage = "Email veya şifre hatalı.";
                        return View("~/Views/Login/LoginProcess.cshtml");
                    }
                }
                else
                {
                    @ViewBag.LErrorMessage = "Tüm alanları doldurunuz.";
                    return View("~/Views/Login/LoginProcess.cshtml");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var responseUserModel = userProcess.SelectUserModelByEmailAndPassword(TxtEmail, TxtPassword);
            Session["CurrentUser"] = responseUserModel;
            Session["userId"] = responseUserModel.UserId;
            var currentUser = Session["CurrentUser"] as UsersModel;
            if (currentUser != null)
            {
                if (currentUser.RoleId == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            return RedirectToAction("Index", "Home");


        }

        private Boolean FormValidation()
        {
            string TxtName = Request.Form["TxtName"].ToString();
            string TxtSurname = Request.Form["TxtSurname"].ToString();
            string TxtEmail = Request.Form["TxtEmail"].ToString();
            string TxtPassword = Request.Form["TxtPassword"].ToString();
            string DtDateOfBirth = Request.Form["DtDateOfBirth"].ToString();
            string TxtAddress = Request.Form["TxtAddress"].ToString();
            string slGender = Request.Form["slGender"].ToString();
            if (String.IsNullOrEmpty(TxtName) || String.IsNullOrEmpty(TxtSurname) ||
                String.IsNullOrEmpty(TxtEmail) || String.IsNullOrEmpty(TxtPassword) ||
                String.IsNullOrEmpty(DtDateOfBirth) || String.IsNullOrEmpty(TxtAddress)
                || slGender == "-1")
            {
                return false;
            }

            return true;
        }

        private Boolean SignUpControl()
        {
            string TxtEmail = Request.Form["TxtEmail"].ToString();
            if (!String.IsNullOrEmpty(TxtEmail))
            {
                UsersModel usersModelGet = new UsersModel
                {
                    Email = TxtEmail,
                };

                UserProcess userProcess = new UserProcess();
                var response = userProcess.SignUpControl(usersModelGet);
                if (response == true)
                    return true;
                else
                    return false;
            }
            return true;
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("LoginProcess");
        }
    }
}