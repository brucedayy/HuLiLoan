using HuLiLoan.App_Start.FilterAuthorize;
using HuLiLoan.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HuLiLoan.Controllers
{
    public class BBSController : Controller
    {
        // GET: BBS
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetArtileByPage(int page,int count)
        {
            var article = BBSProcess.GetArticleJsonByPage(page,count);
            return Content(article);
        }

        public ActionResult GetArticleCount()
        {
            return Content(BBSProcess.GetAllArticleCount().ToString());
        }

        //发表评论
        [HttpPost]
        public ActionResult PublishComment()
        {
            return View();
        }

    }
}