using EShopper.Layers;
using EShopper.Models;
using System;
using System.Globalization;
using System.Linq;
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
        public ActionResult AddOrder(string subtotal, string paymentType, string address,
            string description, string phoneNumber, string cardNumber, string cardName, string cardLastDate, string cardSecurityNumber)
        {
            string userId = Session["userId"].ToString();
            OrderModel orderModel = new OrderModel()
            {
                UserId = Convert.ToInt32(Session["userId"]),
                SubTotal = decimal.Parse(subtotal, new NumberFormatInfo() { NumberDecimalSeparator = "." }),
                PaymentType = Convert.ToInt32(paymentType),
                Address = address,
                Description = description,
                PhoneNumber = phoneNumber,
                CreditCardNumber = cardNumber,
                CreditCardName = cardName,
                CreditCardLastDate = cardLastDate,
                CreditCardSecurityNumber = cardSecurityNumber

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
            //ViewBag bir nesnedir ve return View() çağrısında bir ViewBag nesnesini bir model olarak kullanamazsın. 
        }
    }
}