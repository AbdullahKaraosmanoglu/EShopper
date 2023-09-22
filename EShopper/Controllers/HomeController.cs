using EShopper.Layers;
using EShopper.Models;
using System.Linq;
using System.Web.Mvc;

namespace EShopper.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var session = Session["userId"];
            ProductProcess productProcess = new ProductProcess();

            var GetProducts = productProcess.GetAllProduct();

            var nikeProducts = GetProducts.Where(p => p.ProductBrandName == "Nike").ToList();//bunlar sp yazılarak da yapılabilir
            var adidasProducts = GetProducts.Where(p => p.ProductBrandName == "Adidas").ToList();//bunlar sp yazılarak da yapılabilir

            var productListModel = new ProductListModel
            {
                ProductModel = new ProductModel
                {
                    ProductList = GetProducts,
                },
                NikeProductsCount = nikeProducts.Count,
                AdidasProductsCount = adidasProducts.Count,

            };

            ViewBag.NikeProducts = nikeProducts;
            ViewBag.AdidasProducts = adidasProducts;

            return View(productListModel);
        }





    }
}