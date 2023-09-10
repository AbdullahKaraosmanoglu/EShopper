using EShopper.Layers;
using System.Web.Mvc;

namespace EShopper.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ProductProcess productProcess = new ProductProcess();
            var GetProducts = productProcess.GetAllProduct();
            ViewBag.ProductList = GetProducts;
            var session = Session["userId"];
            return View(ViewBag);
        }


    }
}