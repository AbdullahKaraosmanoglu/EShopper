using EShopper.Layers;
using EShopper.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
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
        //örnek region kullanımı
        #region Insert method  
        /// <summary>
        /// Müşteriyi Üye Yapan Controller Methodudur...
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// 
        //tüm alanları doldurmadan add Usera geçmesini bekleyemezsin. 
        //çünkü parametre olarak model alıyorsun. Frontend tarafında model oluşmadan
        //yani tüm alanlar entity oluşturmadan model oluşmaz. 
        //model oluşmadığu içinde httppost metodu yerine get metodunu arar


        public ActionResult AddUser()
        {
            return RedirectToAction("AddUser", "Login", FormMethod.Post);
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

                int gender= 0;
                
                switch (slGender)
                {
                    case "Man":
                        gender = 0 ;
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
        #endregion

        /// <summary>
        /// Giriş Yap Formunda üyenin olup olmadığını kontrol eden methodum...
        /// </summary>
        /// <param name="usersModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginControl(UsersModel usersModel)
        {
            UserProcess userProcess = new UserProcess();
            string TxtEmail = Request.Form["TxtEmail"].ToString();
            string TxtPassword = Request.Form["TxtPassword"].ToString();
            try
            {

                if (!String.IsNullOrEmpty(TxtEmail) || !String.IsNullOrEmpty(TxtPassword)/*ModelState.IsValid*/)
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
            //ViewBag.UserModel = responseUserModel;
            Session["userId"] = responseUserModel.UserId;
            //ProductProcess productProcess = new ProductProcess();
            //var GetProducts = productProcess.GetAllProduct();
            //ViewBag.ProductList = GetProducts;
            return RedirectToAction("Index", "Home");
            //return View("~/Views/Home/Index.cshtml", ViewBag);


        }

        private Boolean FormValidation()
        {
            string TxtName = Request.Form["TxtName"].ToString();
            string TxtSurname = Request.Form["TxtSurname"].ToString();
            string TxtEmail = Request.Form["TxtEmail"].ToString();
            string TxtPassword = Request.Form["TxtPassword"].ToString();
            string DtDateOfBirth = Request.Form["DtDateOfBirth"].ToString();
            string TxtAddress = Request.Form["TxtAddress"].ToString();
            if (String.IsNullOrEmpty(TxtName) || String.IsNullOrEmpty(TxtSurname) ||
                String.IsNullOrEmpty(TxtEmail) || String.IsNullOrEmpty(TxtPassword) ||
                String.IsNullOrEmpty(DtDateOfBirth) || String.IsNullOrEmpty(TxtAddress))
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
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}