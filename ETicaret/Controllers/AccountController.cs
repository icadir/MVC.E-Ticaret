using ETicaret.Models;
using ETicaret.Models.Account;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ETicaret.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModels user)
        {
            try
            {
                if (user.rePassword != user.Member.Password)
                {
                    throw new Exception("Şifreler Aynı degiltir.");
                }
                if (context.Members.Any(x => x.Email == user.Member.Email))
                {
                    throw new Exception("Bu E-posta Adresi Kayıtlıdır..");
                }
                user.Member.MemberType = DB.MemberTypes.Customer;
                user.Member.AddedDate = DateTime.Now;
                context.Members.Add(user.Member);
                context.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                ViewBag.ReError = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModels model)
        {
            try
            {
                var user = context.Members.FirstOrDefault(x => x.Password == model.Member.Password && x.Email == model.Member.Email);
                if (user != null)
                {
                    Session["LogonUser"] = user;
                    return RedirectToAction("index", "i");
                }
                else
                {
                    ViewBag.ReError = ("Kullanıcı bilgileriniz yanlış");
                    return View();
                }

            }
            catch (Exception ex)
            {

                ViewBag.ReError = ex.Message;
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["LogonUser"] = null;
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult Profil(int id = 0)
        {
            if (id == 0)
            {
                id = base.CurrentUserId();
            }
            var user = context.Members.FirstOrDefault(x => x.Id == id);
            if (user == null) return RedirectToAction("index", "i");
            ProfilModels model = new ProfilModels()
            {
                Members = user,
            };

            return View(model);
        }
    }
}