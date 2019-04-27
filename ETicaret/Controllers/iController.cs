using System.Web.Mvc;

namespace ETicaret.Controllers
{
    public class iController : Controller
    {
        private ETicaretEntities context;

        public iController()
        {
            context = new ETicaretEntities();
        }

        public ActionResult Index()
        {
            
            return View();
        }
    }
}