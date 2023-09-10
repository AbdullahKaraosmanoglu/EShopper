using EShopper.Layers;
using System.Linq;
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

        public void AddCart(string productId)
        {
            string userId = Session["userId"].ToString();
            CartProcess cartProcess = new CartProcess();
            var response = cartProcess.AddCart(productId, userId);
            if (!response)
                return;
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

        public void DeleteItemCart(string productId)
        {
            string userId = Session["userId"].ToString();
            CartProcess cartProcess = new CartProcess();
            var response = cartProcess.DeleteItemCart(productId, userId);
            if (!response)
                return;
        }
    }
}