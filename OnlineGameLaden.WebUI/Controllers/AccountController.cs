using OnlineGameLaden.WebUI.Models;
using OnlineGameLaden.WebUI.Util.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineGameLaden.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;

        public AccountController(IAuthProvider authProvider)
        {
            this.authProvider = authProvider;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string retUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(retUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Sie haben einen falschen Benutzernamen oder ein falsches Passwort eingegeben");
                    return View();
                }
            }
            else
                return View();
        }
        // GET: Account
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}