using EShopper.Layers;
using EShopper.Models;
using System.Linq;
using System.Web.Mvc;

namespace EShopper.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Indexs()
        {
            ProductProcess productProcess = new ProductProcess();

            var session = Session["userId"];

            var GetProducts = productProcess.GetAllProduct();

            var productModel = new ProductModel
            {
                ProductList = GetProducts,
            };

            return View(productModel);
        }
    }
}