using ETicaret.DB;
using System.Linq;
using System.Web.Mvc;

namespace ETicaret.Controllers
{
    public class BaseController : Controller
    {
        protected ETicaretDbEntities context { get; private set; }
        // GET: Base
        public BaseController()
        {
            context = new ETicaretDbEntities();
            ViewBag.MenuCategories = context.Categories.Where(x => x.Parent_Id == null).ToList();
        }

        protected DB.Members GCurrentUser()
        {
            if (Session["LogonUser"] == null) return null;
            return (DB.Members)Session["LogonUser"];
        }

        protected int CurrentUserId()
        {
            if (Session["LogonUser"] == null) return 0;
            return ((DB.Members)Session["LogonUser"]).Id;
        }
    }
}