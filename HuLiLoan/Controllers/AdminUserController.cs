using HuLiLoan.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HuLiLoan.Controllers
{
    public class AdminUserController : Controller
    {
        // GET: AdminUser
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (AdminUserProcess.Login(username, password))
            {
                Session["adminuser"] = username;
                return new RedirectResult("/Home/AdminUI");
            }
            else return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["adminuser"] = null;
            return RedirectToAction("Login");
        }

    }
}