using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ETicaret.DB;
using ETicaret.Models.Message;

namespace ETicaret.Controllers
{
    public class MesageController : BaseController
    {
        // GET: Mesage
        public ActionResult i()
        {
            if (IsLogon() == false) return RedirectToAction("index", "i");
            var currentId = CurrentUserId();
            var model = new iModels();
            model.Users = new List<SelectListItem>();
            var users = context.Members.Where(x => ((int)x.MemberType) > 0 && x.Id != currentId).ToList();
            model.Users = users.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.Name} {x.Surname} {x.MemberType.ToString()}"
            }).ToList();

            return View(model);
        }

        public ActionResult SendMessage(SendMessageModel message)
        {
            if (IsLogon() == false) return RedirectToAction("index", "i");
            DB.Messages mesaj = new Messages
            {
                Id = Guid.NewGuid(),
                AddedDate = DateTime.Now,
                IsRead = false,
                Subject = message.Subject
            };
            var mRep = new DB.MessageReplies
            {
                Id = Guid.NewGuid(),
                AddedDate = DateTime.Now,
                Member_Id = CurrentUserId(),
                Text = message.MessageBody
            };
            mesaj.MessageReplies.Add(mRep);
            context.Messages.Add(mesaj);
            context.SaveChanges();
            return RedirectToAction("i", "Mesage");
        }
    }
}