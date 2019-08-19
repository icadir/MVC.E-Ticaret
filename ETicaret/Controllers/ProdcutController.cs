using System;
using System.Data.Entity;
using System.IO;
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
            var productImagePath = string.Empty;
            if (Request.Files != null && Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file.ContentLength > 0)
                {
                    var folder = Server.MapPath("~/images/Product");
                    var fileName = Guid.NewGuid() + ".jpg";
                    file.SaveAs(Path.Combine(folder, fileName));

                    var filePath = "/images/Product/" + fileName;
                    productImagePath = filePath;
                }
            }
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
                if (string.IsNullOrEmpty(productImagePath)==false)
                {
                    dbProduct.ProductImageName = productImagePath;
                }
            }
            else
            {
                product.AddedDate = DateTime.Now;
                product.ProductImageName = productImagePath;
                context.Entry(product).State = EntityState.Added;
            }
     
            context.SaveChanges();
            return RedirectToAction("i");
        }
    }
}