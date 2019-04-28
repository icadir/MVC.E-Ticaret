using ETicaret.Models.i;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ETicaret.Controllers
{
    public class iController : BaseController
    {
        [HttpGet]
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
        [HttpGet]
        public ActionResult Product(int id = 0)
        {
            var pro = context.Products.FirstOrDefault(x => x.Id == id);

            if (pro == null) return RedirectToAction("index", "i");

            ProductModels model = new ProductModels()
            {
                Product = pro,
                Comments = pro.Comments.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Product(DB.Comments comment)
        {
            try
            {
                comment.Member_Id = base.CurrentUserId();
                comment.AddedDate = DateTime.Now;
                context.Comments.Add(comment);
                context.SaveChanges();
                return RedirectToAction("Product", "i");

            }
            catch (Exception ex)
            {
                ViewBag.ReError = ex.Message;
                return View(comment);

            }

        }
    }

}


