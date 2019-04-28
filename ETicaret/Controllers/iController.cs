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
            ViewBag.MenuCategories = context.Categories.Where(x => x.Parent_Id == null).ToList();
        }

        public ActionResult Index(int? id)
        {
            IQueryable<DB.Products> products = context.Products;
            DB.Categories category = null;
            if (id.HasValue)
            {
                products = products.Where(x => x.Category_Id == id.Value);
                category = context.Categories.FirstOrDefault(x => x.Id == id.Value);
            }
            var viewModel = new Models.i.indexModel()
            {
                Products = products.ToList(),
                Category = category
            };
            return View(viewModel);
        }

    }

}


