using HuLiLoan.App_Start.FilterAuthorize;
using HuLiLoan.BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HuLiLoan.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Register   
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // GET: Log out
        [HttpGet]
        public ActionResult Logout()
        {
            Session["username"] = null;
            return RedirectToAction("Index");
        }

        // Post: Register User
        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            if (UserProcess.Register(username, password))
                return RedirectToAction("Login");
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["username"] == null)
                return View();
            else
                return View("Index");
        }

        // Post: Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (UserProcess.Login(username, password))
            {
                Session["username"] = username;
                return RedirectToAction("Index");
            }
            else return View();
        }

        // GET: ApplyLoan
        [HttpGet]
        public ActionResult ApplyLoan()
        {
            if (Session["username"] == null)
                return View("Login");
            return View();
        }


        [HttpPost]
        public ActionResult SummerNoteFileUpLoad()
        {
            var username = Session["username"].ToString();
            var fiPath = "\\SummerNoteFileUpLoad\\";
            var file = Request.Files["file"];
            if (FileHelper.UpLoadSummerNoteFile(username, file))
                return Content(fiPath + "\\" + username + "\\" + file.FileName);
            return View();
        }


        [HttpPost]
        public ActionResult ApplyLoan(string name, string idcard, string cellphone, string province, string city,
            string district, string address, string imgIDplus, string imgIDminus, string imgHandPlus, string applytable)
        {
            var username = Session["username"].ToString();
            var fimgIDplus = Request.Files["imgIDplus"];
            var fimgIDminus = Request.Files["imgIDminus"];
            var fimgHandPlus = Request.Files["imgHandPlus"];
            var fapplytable = Request.Files["applytable"];
            var fiPath = "\\FileUpLoad\\" + username + "\\";
            if (FileHelper.UpLoadApplyLoanFile(username, fimgIDplus)
                && FileHelper.UpLoadApplyLoanFile(username, fimgIDminus)
                && FileHelper.UpLoadApplyLoanFile(username, fimgHandPlus)
                && FileHelper.UpLoadApplyLoanFile(username, fapplytable))
            {
                if (LoanProcess.AddLoan(username, name, idcard, cellphone, province + "_" + city + "_" + district + "_" + address,
                    fiPath + fimgIDplus.FileName, fiPath + fimgIDminus.FileName, fiPath + fimgHandPlus.FileName, fiPath + fapplytable.FileName))
                    //return RedirectToAction("/HomeHelper/ApplyLoanSuccess");
                    return RedirectToRoute(new { controller = "HomeHelper", action = "ApplyLoanSuccess" });
            }
            return View("Error");
        }

        // GET: ReviewAppyLoanAdmin
        [HttpGet]
        public ActionResult ReviewLoan()
        {
            return View();
        }

        // GET: Error
        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BBS()
        {
            return View();
        }

        [HttpGet]
        [CheckUserLogin]
        public ActionResult PublishArticle()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PublishArticle(string title,string details)
        {
            var username = Session["username"].ToString();
            var te = title;
            var de = details;
            if (BBSProcess.PublishArticle(username, te, de))
                return Content("success");
            return View();
        }

        //管理员界面
        [CheckAdminUserLogin]
        public ActionResult AdminUI()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogoutAdmin()
        {
            Session["adminuser"] = null;
            return new RedirectResult("/AdminUser/Login");
        }


    }
}