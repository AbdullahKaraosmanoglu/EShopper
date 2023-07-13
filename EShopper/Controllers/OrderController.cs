using EShopper.Layers;
using EShopper.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EShopper.Controllers
{
    public class OrderController : Controller
    {
        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrder(string subtotal, string paymentType, string address, string description, string phoneNumber)
        {
            string userId = Session["userId"].ToString();
            OrderModel orderModel = new OrderModel()
            {
                UserId = Convert.ToInt32(Session["userId"]),
                SubTotal =  decimal.Parse(subtotal, new NumberFormatInfo() { NumberDecimalSeparator = "." }),
                PaymentType = Convert.ToInt32(paymentType),
                Address = address,
                Description = description,
                PhoneNumber = phoneNumber,
            };
            OrdersProcess ordersProcess = new OrdersProcess();
            ordersProcess.AddOrders(orderModel, userId);

            return RedirectToAction("GetProductToCart", "Cart");
        }

        public ActionResult GetOrder()
        {
            string userId = Session["userId"].ToString();
            OrdersProcess ordersProcess = new OrdersProcess();
            var response = ordersProcess.GetOrder(userId);
            var OrderList = response.ToList();
            ViewBag.OrderList = OrderList;
            return View("~/Views/Orders/Index.cshtml", ViewBag);
        }
    }
}