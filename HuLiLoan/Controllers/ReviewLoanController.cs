using HuLiLoan.App_Start.FilterAuthorize;
using HuLiLoan.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HuLiLoan.Controllers
{
    public class ReviewLoanController : Controller
    {
        // GET: 审贷人管理首页
        [CheckReviewLoanUserLogin]
        public ActionResult Index()
        {
            return View();
        }

        //GET: 审核贷款
        [HttpPost]
        public ActionResult LoanCheck(string lid)
        {
            short ld = Convert.ToInt16(lid);
            if (LoanProcess.ApproveLoanApply(ld))
                return Content("ok");
            return View();
        }

        [HttpGet]
        public ActionResult LoanCheck()
        {          
            return View();
        }

        //GET: 所有贷款
        public ActionResult AllLoan()
        {
            return View();
        }

        //GET: 逾期贷款
        public ActionResult AbnLoan()
        {
            return View();
        }


        //贷款用户管理
        public ActionResult UserAdmin()
        {
            return View();
        }

        //审贷用户管理
        public ActionResult LoanUserAdmin()
        {
            return View();
        }


        //发布新闻
        public ActionResult PublishNews()
        {
            return View();
        }

        //管理历史
        public ActionResult HistoryAdmin()
        {
            return View();
        }

        //网站日志
        public ActionResult WebLog()
        {
            return View();
        }

        //密码管理
        public ActionResult PasswordAdmin()
        {
            return View();
        } 

        //GET: 答疑专区
        public ActionResult AnsQuestion()
        {
            return View();
        }

        //GET: 个人信息
        public ActionResult PersonalInfo()
        {
            return View();            
        }

        //GET：登陆界面
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (CheckLoanUserProcess.Login(username, password))
            {
                Session["loancheckuser"] = username;
                return RedirectToAction("Index");
            }
            else return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["loancheckuser"] = null;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetLoanJson()
        {
            var json=LoanProcess.GetLoanJson();
            return Content(json);
        }

        [HttpGet]
        public ActionResult GetUnCheckedLoanJson()
        {
            var json = LoanProcess.GetUnCheckedLoanJson();
            return Content(json);
        }

        [HttpGet]
        public ActionResult AllLoanApplyInfoUI()
        {
            return View();
        }


    }
}