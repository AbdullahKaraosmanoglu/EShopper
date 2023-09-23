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

            return View();
        }

        public ActionResult ProductsControl()
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

        public ActionResult UsersControl()
        {
            UserProcess userProcess = new UserProcess();

            int pageNumber = 1; // Sayfa numarasını belirleyin
            int pageSize = 7; // Sayfa boyutunu belirleyin

            var session = Session["userId"];

            var getUsers = userProcess.GetAllUsersWithPagenation(pageNumber, pageSize);

            var userModel = new UsersModel
            {
                UserList = getUsers,
            };

            return View(userModel);
        }
    }
}