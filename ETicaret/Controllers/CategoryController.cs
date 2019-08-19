using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Category
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

            var categorieses = context.Categories.Where(x => x.IsDeleted == false || x.IsDeleted == null).ToList();
            return View(categorieses.OrderByDescending(x => x.AddedDate).ToList());
        }
        public ActionResult Edit(int id = 0)
        {
            var cat = context.Categories.FirstOrDefault(x => x.Id == id);
            var cats = context.Categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            cats.Add(new SelectListItem()
            {
                Value ="0",
                Text = "Ana Kategori"
            });
            ViewBag.Categories = cats;

            return View(cats);
        }
        [HttpPost]
        public ActionResult Edit(DB.Categories category)
        {
            if (category.Id > 0)
            {
                var cat = context.Categories.FirstOrDefault(x => x.Id == category.Id);
                cat.Description = category.Description;
                cat.Name = category.Name;
                cat.ModifedDate = DateTime.Now;
                cat.IsDeleted = false;
                if (category.Parent_Id>0)
                    cat.Parent_Id = category.Parent_Id;
                else
                    category.Parent_Id = null;
            }
            else
            {
                category.AddedDate = DateTime.Now;
                category.IsDeleted = false;
                if (category.Parent_Id==0)
                    category.Parent_Id = null;
                context.Entry(category).State = EntityState.Added;
            }
            context.SaveChanges();
            return RedirectToAction("i");
        }
        public ActionResult Delete(int id)
        {
            var category = context.Categories.FirstOrDefault(x => x.Id == id);
            category.IsDeleted = true;
            context.SaveChanges();
            return RedirectToAction("i");
        }
    }
}