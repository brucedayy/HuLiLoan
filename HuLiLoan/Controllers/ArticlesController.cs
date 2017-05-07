using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HuLiLoan.BLL;
using HuLiLoan.App_Start.FilterAuthorize;

namespace HuLiLoan.Controllers
{
    public class ArticlesController : Controller
    {
        private HuLiLoanDBEntities db = new HuLiLoanDBEntities();

        // GET: Articles
        [CheckAdminUserLogin]
        public async Task<ActionResult> Index(int? pg, int? ct)
        {
            int page = 1, count = 10;
            if (pg !=null && ct!= null)
            {
                page = (int)pg;
                count = (int)ct;
            }
            //var article = db.Article.Include(a => a.User);
            var article = (from item in db.Article.Include(a=>a.User)
                           orderby item.Aid
                           select item
                           ).Skip(count * (page - 1)).Take(count);
            return View(await article.ToListAsync());
        }

        // GET: Articles/Details/5
        [CheckAdminUserLogin]
        public async Task<ActionResult> Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Article.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        [CheckAdminUserLogin]
        public ActionResult Create()
        {
            ViewBag.UserName = new SelectList(db.User, "UserName", "Password");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Aid,UserName,Title,Details,PubTime")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Article.Add(article);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserName = new SelectList(db.User, "UserName", "Password", article.UserName);
            return View(article);
        }

        // GET: Articles/Edit/5
        [CheckAdminUserLogin]
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Article.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserName = new SelectList(db.User, "UserName", "Password", article.UserName);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Aid,UserName,Title,Details,PubTime")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserName = new SelectList(db.User, "UserName", "Password", article.UserName);
            return View(article);
        }

        // GET: Articles/Delete/5
        [CheckAdminUserLogin]
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Article.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            Article article = await db.Article.FindAsync(id);
            db.Article.Remove(article);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
