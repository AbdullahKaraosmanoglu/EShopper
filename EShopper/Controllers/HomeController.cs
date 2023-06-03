using EShopper.Layers;
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
            ////UsersModel model = new UsersModel();
            //Guid guid = Guid.NewGuid();
            //var x = guid.ToString();
            ProductProcess productProcess = new ProductProcess();
            var GetProducts = productProcess.GetAllProduct();
            ViewBag.ProductList = GetProducts;
            return View();
        }
    }
}