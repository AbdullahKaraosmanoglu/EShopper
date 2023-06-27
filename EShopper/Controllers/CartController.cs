using EShopper.Layers;
using EShopper.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShopper.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Cart()
        {
            return View();
        }

        public void AddCart(string productId, string ImagePath)
        {
            string userId = Session["userId"].ToString();
            CartProcess cartProcess = new CartProcess();
            var response = cartProcess.AddCart(productId, userId, ImagePath);
            if (!response)
            {
            }
            //ProductProcess productProcess = new ProductProcess();
            //var GetProducts = productProcess.GetAllProduct();
            //ViewBag.ProductList = GetProducts;
            //var session = Session["userId"];
            return;
            //return RedirectToAction("Index", "Home");
            //return View("~/Views/Home/Index.cshtml", ViewBag);
        }

        public ActionResult GetProductToCart()
        {
            string userId = Session["userId"].ToString();
            CartProcess cartProcess = new CartProcess();
            var response = cartProcess.GetCarts(userId);
            var CartList = response.ToList();
            ViewBag.CartList = CartList;

            return View("~/Views/Cart/Cart.cshtml", ViewBag);
        }
    }
}