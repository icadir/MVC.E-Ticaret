using ETicaret.DB;
using System.Linq;
using System.Web.Mvc;

namespace ETicaret.Controllers
{
    public class iController : Controller
    {
        private ETicaretDbEntities context;

        public iController()
        {
            context = new ETicaretDbEntities();
        }

        public ActionResult Index()
        {
            var viewModel = new Models.i.indexModel()
            {
                Products = context.Products.ToList()
            };
            return View(viewModel);
        }

    }

}


