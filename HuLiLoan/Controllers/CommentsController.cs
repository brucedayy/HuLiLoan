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

namespace HuLiLoan.Controllers
{
    public class CommentsController : Controller
    {
        private HuLiLoanDBEntities db = new HuLiLoanDBEntities();

        // GET: Comments
        public async Task<ActionResult> Index()
        {
            var comment = db.Comment.Include(c => c.Article).Include(c => c.User);
            return View(await comment.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<ActionResult> Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comment.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.Aid = new SelectList(db.Article, "Aid", "UserName");
            ViewBag.UserName = new SelectList(db.User, "UserName", "Password");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Cid,Aid,UserName,Title,Details,PubTime")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comment.Add(comment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Aid = new SelectList(db.Article, "Aid", "UserName", comment.Aid);
            ViewBag.UserName = new SelectList(db.User, "UserName", "Password", comment.UserName);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comment.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Aid = new SelectList(db.Article, "Aid", "UserName", comment.Aid);
            ViewBag.UserName = new SelectList(db.User, "UserName", "Password", comment.UserName);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Cid,Aid,UserName,Title,Details,PubTime")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Aid = new SelectList(db.Article, "Aid", "UserName", comment.Aid);
            ViewBag.UserName = new SelectList(db.User, "UserName", "Password", comment.UserName);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comment.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            Comment comment = await db.Comment.FindAsync(id);
            db.Comment.Remove(comment);
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
