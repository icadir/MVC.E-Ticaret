using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ETicaret.Controllers
{
    public class ProdcutController : BaseController
    {
        // GET: Prodcut
        public ActionResult i()
        {
            DB.Members user = null;
            if (IsLogon() == true)
            {
                user = CurrentUser();
            }
            if (IsLogon() == false)
            {
                return RedirectToAction("index", "i");
            }
            else if (user.MemberType > 4)
            {
                return RedirectToAction("index", "i");
            }
            var products = context.Products.ToList();
            return View(products);
        }

        public ActionResult Edit(int id)
        {
            var product = context.Products.FirstOrDefault(x => x.Id == id);
            var categories = context.Categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToString();
            ViewBag.Categories = categories;
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(DB.Products product)
        {
            product.ProductImageName = string.Empty;
            if (product.Id>0)
            {
                var dbProduct = context.Products.FirstOrDefault(x => x.Id == product.Id);
                dbProduct.Category_Id = product.Category_Id;
                dbProduct.ModifiedDate = DateTime.Now;
                dbProduct.Description = product.Description;
                dbProduct.IsContinued = product.IsContinued;
                dbProduct.Name = product.Name;
                dbProduct.Price = product.Price;
                dbProduct.UnitsInStock = product.UnitsInStock;
            }
            else
            {
                product.AddedDate = DateTime.Now;
                context.Entry(product).State = EntityState.Added;
            }
            context.SaveChanges();
            return RedirectToAction("i");
        }
    }
}