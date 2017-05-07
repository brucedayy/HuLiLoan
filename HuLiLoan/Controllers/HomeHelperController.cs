using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HuLiLoan.Controllers
{
    public class HomeHelperController : Controller
    {
        // GET: HomeHelper
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ApplyLoanSuccess()
        {
            return View();
        }
    }
}